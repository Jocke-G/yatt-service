using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Yatt_Service.Contracts;
using Yatt_Service.Mapping;
using Yatt_Service.Services;

namespace Yatt_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(ILogger<ToDoController> logger, UserService service) : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger = logger;
        private readonly UserService _service = service;

        [Authorize]
        [HttpGet("/me", Name = "ReadAuthenticatedUser")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserContract), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<UserContract> Get()
        {
            var userId = User.GetUserId();
            _logger.LogInformation("Read authenticated user {userId}", userId);
            var entity = await _service.GetUser(userId);
            return entity.ToContract();
        }
    }
}
