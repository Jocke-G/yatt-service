using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Yatt_Service.Contracts;
using Yatt_Service.Services;

namespace Yatt_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController(ToDoService _service) : ControllerBase
    {
        [Authorize]
        [HttpPost(Name = nameof(CreateToDo))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ToDoItemContract>> CreateToDo([FromBody] ToDoItemContract contract)
        {
            var created = await _service.AddAsync(contract);
            return CreatedAtRoute(nameof(ReadToDo), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpGet(Name = nameof(ReadUsersToDos))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ToDoItemContract>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ToDoItemContract>>> ReadUsersToDos()
        {
            return Ok(await _service.GetAllForUserAsync());
        }

        [Authorize]
        [HttpGet("{id}", Name = nameof(ReadToDo))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ToDoItemContract>> ReadToDo(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [Authorize]
        [HttpPut("{id}", Name = nameof(UpdateToDo))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ToDoItemContract>> UpdateToDo(Guid id, [FromBody] ToDoItemContract contract)
        {
            return Ok(await _service.UpdateAsync(id, contract));
        }

        [Authorize]
        [HttpDelete("{id}", Name = nameof(DeleteToDo))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteToDo(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
