using System.Web.Http;
using Authorization.Api.Models;
using Authorization.Api.Services;

namespace Authorization.Api.Controllers
{
    public class AccountController : ApiController
    {
        private readonly SecurityService securityService;

        public AccountController()
        {
            securityService = new SecurityService();
        }

        [HttpPost]
        public IHttpActionResult Login(Login model)
        {
            var result = securityService.Login(model);

            return Ok(result);
        }
    }
}
