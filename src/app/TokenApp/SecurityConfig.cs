using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode;

namespace TokenApp
{
    public class SecurityConfig
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = SecuritySetting.TokenIssuerName;
            var audience = SecuritySetting.ClientId;
            var securityKey = TextEncodings.Base64Url.Decode(SecuritySetting.SecurityKey);

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, securityKey)
                    },
                    Provider = new OAuthBearerAuthenticationProvider
                    {
                        OnValidateIdentity = context =>
                        {
                            var userData = context.Ticket.Identity.Claims.FirstOrDefault(s => s.Type == ClaimTypes.UserData);
                            if (userData == null || userData.Value != "IsValid")
                            {
                                context.Rejected();
                            }

                            return Task.FromResult<object>(null);
                        }
                    }
                });
        }

    }
}
