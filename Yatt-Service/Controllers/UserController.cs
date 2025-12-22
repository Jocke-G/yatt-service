using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Yatt_Service.Contracts;
using Yatt_Service.Services;

namespace Yatt_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(UserService _service) : ControllerBase
    {
        [Authorize]
        [HttpGet("me", Name = nameof(ReadAuthenticatedUser))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserContract), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserContract>> ReadAuthenticatedUser()
        {
            return Ok(await _service.GetUserAsync());
        }
    }
}
