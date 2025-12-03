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
    public class ToDoController(ILogger<ToDoController> logger, ToDoService service) : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger = logger;
        private readonly ToDoService _service = service;

        [HttpPost(Name = "CreateToDo")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ToDoItemContract toDoItem)
        {
            var userId = User.GetUserId();

            if (toDoItem == null)
            {
                return BadRequest("ToDo item cannot be null.");
            }
            var entity = toDoItem.ToEntity();
            entity.UserId = userId;
            var createdToDo = await _service.AddAsync(entity);
            return CreatedAtRoute("ReadToDoById", new { id = createdToDo.Id }, createdToDo.ToContract());
        }

        [Authorize]
        [HttpGet(Name = "ReadToDos")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<ToDoItemContract>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<ToDoItemContract>> Get()
        {
            var userId = User.GetUserId();
            _logger.LogDebug("Read ToDos for user {userId}", userId);
            var todos = await _service.GetAllForUserAsync(userId);
            return todos.ToContracts();
        }

        [Authorize]
        [HttpGet("{id}", Name = "ReadToDoById")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid id)
        {
            var userId = User.GetUserId();

            var todo = await _service.GetByIdAsync(userId, id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo.ToContract());
        }

        [HttpPut("{id}", Name = "UpdateToDo")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] ToDoItemContract contract)
        {
            var userId = User.GetUserId();

            var existing = await _service.GetByIdAsync(userId, id);
            if (existing == null)
            {
                return NotFound();
            }
            var entity = existing.PatchFromContract(contract);
            await _service.UpdateAsync(entity);
            return Ok(entity);
        }

        [HttpDelete("{id}", Name = "DeleteToDo")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();
            var existingToDo = await _service.GetByIdAsync(userId, id);
            if (existingToDo == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(userId, id);
            return NoContent();
        }
    }
}
