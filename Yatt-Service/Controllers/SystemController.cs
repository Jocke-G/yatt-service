using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Yatt_Service.Contracts;

namespace Yatt_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : ControllerBase
    {
        [HttpGet("version", Name = nameof(ReadVersion))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<VersionContract> ReadVersion()
        {
            return Ok(new VersionContract
            {
                Version = GetType().Assembly.GetName().Version?.ToString(),
            });
        }
    }
}
