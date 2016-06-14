using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Authorization.Api.Services;

namespace Authorization.Api.Providers
{
    public class AuthorizationProvider : OAuthAuthorizationServerProvider
    {
        private const string InvalidClientId = "invalid_clientId";
        private const string InvalidGrant = "invalid_grant";

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError(InvalidClientId, "client_Id is not set");
                return Task.FromResult<object>(null);
            }

            var audience = AudienceService.FindAudience(context.ClientId);

            if (audience == null)
            {
                context.SetError(InvalidClientId, string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var validUser = new UserService().Get(context.UserName, context.Password);
            if (validUser == null)
            {
                context.SetError(InvalidGrant, "The user name or password is incorrect");
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.UserData, "IsValid", ClaimValueTypes.String, "(local)"));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            foreach (var role in validUser.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "audience", context.ClientId ?? string.Empty }
            });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }
    }
}