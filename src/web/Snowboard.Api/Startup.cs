using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using TokenApp;

[assembly: OwinStartup(typeof(Snowboard.Api.Startup))]

namespace Snowboard.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            config.MapHttpAttributeRoutes();
        }

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
