using System.Web.Http;
using Snowboard.Api.Services;

namespace Snowboard.Api.Controllers
{
    [Authorize]
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
    }
}
