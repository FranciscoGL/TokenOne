using System.Collections.Generic;
using System.Linq;
using Authorization.Api.Helpers;
using Authorization.Api.Models;

namespace Authorization.Api.Services
{
    public class UserService
    {
        private const string SaltKey = "malebolgia";

        private static readonly List<User> Data;

        static UserService()
        {
            Data = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Fran01",
                    Email = "fran01@spawn.com",
                    Password = PasswordHashHelper.GetPasswordHashAndSalt(string.Format("{0}{1}", "hellspawn", SaltKey)),
                    PasswordSalt = SaltKey,
                    Roles = new List<string>{ RoleConstants.Mananger }
                },
                new User
                {
                    Id = 2,
                    Name = "Supervisor",
                    Email = "supervisor@spawn.com",
                    Password = PasswordHashHelper.GetPasswordHashAndSalt(string.Format("{0}{1}", "hellspawn", SaltKey)),
                    PasswordSalt = SaltKey,
                    Roles = new List<string>{ RoleConstants.Mananger, RoleConstants.Supervisor }
                }
            };
        }

        public List<User> GetAll()
        {
            return Data;
        }

        public User Get(string email)
        {
            return Data.FirstOrDefault(s => s.Email == email);
        }

        public User Get(string email, string password)
        {
            User user = null;

            var resultUser = Get(email);

            if (resultUser == null)
                return null;

            var saltForUser = resultUser.PasswordSalt;

            var passwordAndSalt = password + saltForUser;

            var hash = PasswordHashHelper.GetPasswordHashAndSalt(passwordAndSalt);

            var validUser = Data.FirstOrDefault(s => s.Email == email && s.Password == hash);

            if (validUser != null)
            {
                user = validUser;
            }

            return user;
        }

    }
}