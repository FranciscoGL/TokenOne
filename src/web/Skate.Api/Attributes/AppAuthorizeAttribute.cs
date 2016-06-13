using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Security.Tokens;
using System.Web.Http.Controllers;
using System.Security.Claims;
using TokenApp;
using TokenApp.Helpers;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace Skate.Api.Attributes
{
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        private const string TokenKeyName = "token";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;

            var token = headers.GetValues(TokenKeyName).FirstOrDefault();

            if (string.IsNullOrEmpty(token))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                return;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = AuthorizationHelper.GetBytes(SecuritySetting.SecurityKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidAudience = SecuritySetting.AudienceAddress,
                IssuerSigningToken = new BinarySecretSecurityToken(securityKey),
                ValidIssuer = SecuritySetting.TokenIssuerName
            };
            try
            {
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                var userData = principal.Claims.FirstOrDefault(s => s.Type == ClaimTypes.UserData);
                if (userData != null)
                {
                    var isValid1 = userData.Value;
                    if (isValid1 != "IsValid")
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }
        }
    }
}
