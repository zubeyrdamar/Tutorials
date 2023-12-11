using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult List()
        {
            return Ok();
        }
    }
}
