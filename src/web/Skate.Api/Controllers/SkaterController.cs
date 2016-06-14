using System.Web.Http;
using Skate.Api.Services;

namespace Skate.Api.Controllers
{
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

        [Authorize(Roles = "Supervisor")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var result = service.Get(id);

            return Ok(result);
        }
    }
}
