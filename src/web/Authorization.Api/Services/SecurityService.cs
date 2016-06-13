using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using Authorization.Api.Helpers;
using Authorization.Api.Models;
using TokenApp;
using TokenApp.Helpers;

namespace Authorization.Api.Services
{
    public class SecurityService
    {
        private readonly UserService userService;

        public SecurityService()
        {
            userService = new UserService();
        }

        public Login Login(Login model)
        {
            var validUser = userService.Get(model.Email, model.Password);
            if (validUser == null)
            {
                return null;
            }

            var token = GetJwtToken(validUser);

            return new Login { AccessToken = token };

        }

        public string GetJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = AuthorizationHelper.GetBytes(SecuritySetting.SecurityKey);
            var now = DateTime.UtcNow;

            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.UserData, "IsValid", ClaimValueTypes.String, "(local)"));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                TokenIssuerName = SecuritySetting.TokenIssuerName,
                AppliesToAddress = SecuritySetting.AudienceAddress,
                Lifetime = new Lifetime(
                    now, 
                    now.AddMinutes(60)),
                    SigningCredentials = new SigningCredentials(
                        new InMemorySymmetricSecurityKey(securityKey),
                        "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                        "http://www.w3.org/2001/04/xmlenc#sha256")
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}