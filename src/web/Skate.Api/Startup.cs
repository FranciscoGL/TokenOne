using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using TokenApp;
using TokenApp.Helpers;

[assembly: OwinStartup(typeof(Skate.Api.Startup))]

namespace Skate.Api
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
            var audience = SecuritySetting.AudienceAddress;
            var securityKey = AuthorizationHelper.GetBytes(SecuritySetting.SecurityKey);

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
