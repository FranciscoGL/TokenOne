using System.Web.Http;
using Snowboard.Api.Services;

namespace Snowboard.Api.Controllers
{
    public class SnowboarderController : ApiController
    {
        private readonly SnowboarderService service;

        public SnowboarderController()
        {
            service = new SnowboarderService();
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
