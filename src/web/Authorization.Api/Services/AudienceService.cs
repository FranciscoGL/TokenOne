using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using Authorization.Api.Models;
using Microsoft.Owin.Security.DataHandler.Encoder;
using TokenApp;

namespace Authorization.Api.Services
{
    public class AudienceService
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList =
            new ConcurrentDictionary<string, Audience>();

        static AudienceService()
        {
            AudiencesList.TryAdd(SecuritySetting.ClientId,
                new Audience
                {
                    ClientId = SecuritySetting.ClientId,
                    SecretKey = SecuritySetting.SecurityKey,
                    Name = "SecurityFlowPrototype"
                });
        }

        private static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);

            var base64Secret = TextEncodings.Base64Url.Encode(key);

            var newAudience = new Audience {ClientId = clientId, SecretKey = base64Secret, Name = name};
            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }

        public static Audience FindAudience(string clientId)
        {
            Audience audience;

            return AudiencesList.TryGetValue(clientId, out audience) 
                ? audience 
                : null;
        }
    }
}