using Microsoft.AspNetCore.Mvc;

namespace Yatt_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        [HttpGet("verison", Name = "GetVersion")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetVersion()
        {
            var version = GetType().Assembly.GetName().Version.ToString();
            return Ok(new {
                version = version,
            });
        }
    }
}
