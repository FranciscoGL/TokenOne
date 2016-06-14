using Microsoft.Owin.Security;
using System;
using System.IdentityModel.Tokens;
using Authorization.Api.Services;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace Authorization.Api.Formats
{
    public class AuthorizationJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string AudiencePropertyKey = "audience";

        private readonly string issuer;

        public AuthorizationJwtFormat(string issuer)
        {
            this.issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            var audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) 
                ? data.Properties.Dictionary[AudiencePropertyKey] 
                : null;

            if (string.IsNullOrWhiteSpace(audienceId)) 
                throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            var audience = AudienceService.FindAudience(audienceId);

            var signingKey = new HmacSigningCredentials(TextEncodings.Base64Url.Decode(audience.SecretKey));

            var issued = data.Properties.IssuedUtc ?? DateTimeOffset.UtcNow;
            var expires = data.Properties.ExpiresUtc ?? DateTimeOffset.UtcNow;

            var token = new JwtSecurityToken(
                issuer,
                audienceId,
                data.Identity.Claims,
                issued.UtcDateTime,
                expires.UtcDateTime,
                signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}