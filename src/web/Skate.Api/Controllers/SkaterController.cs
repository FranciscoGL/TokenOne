using System.Web.Http;
using Skate.Api.Services;

namespace Skate.Api.Controllers
{
    [Authorize]
    public class SkaterController : ApiController
    {
        private readonly SkaterService service;

        public SkaterController()
        {
            service = new SkaterService();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var result = service.Get();

            return Ok(result);
        }
    }
}
