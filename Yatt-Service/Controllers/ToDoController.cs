using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Yatt_Service.Contracts;
using Yatt_Service.Mapping;
using YattService.Common.RepositoryInterfaces;

namespace Yatt_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoItemRepository _repository;

        public ToDoController(ILogger<ToDoController> logger, IToDoItemRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost(Name = "CreateToDo")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ToDoItemContract toDoItem)
        {
            string userId = GetUserId();

            if (toDoItem == null)
            {
                return BadRequest("ToDo item cannot be null.");
            }
            var entity = toDoItem.ToEntity();
            entity.UserId = userId;
            var createdToDo = await _repository.AddAsync(entity);
            return CreatedAtRoute("ReadToDoById", new { id = createdToDo.Id }, createdToDo.ToContract());
        }

        [Authorize]
        [HttpGet(Name = "ReadToDos")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ToDoItemContract>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ToDoItemContract>), StatusCodes.Status401Unauthorized)]
        public async Task<IEnumerable<ToDoItemContract>> Get()
        {
            var userId = GetUserId();
            _logger.LogInformation("Read ToDos for user {userId}", userId);
            var todos = await _repository.GetAllForUserAsync(userId);
            return todos.ToContracts();
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new InvalidOperationException("Authenticated user has no NameIdentifier claim.");
        }

        [HttpGet("{id}", Name = "ReadToDoById")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid id)
        {
            var userId = GetUserId();

            var todo = await _repository.GetByIdAsync(userId, id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo.ToContract());
        }

        

        [HttpPut("{id}", Name = "UpdateToDo")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] ToDoItemContract contract)
        {
            var userId = GetUserId();

            var existing = await _repository.GetByIdAsync(userId, id);
            if (existing == null)
            {
                return NotFound();
            }
            var entity = existing.PatchFromContract(contract);
            await _repository.UpdateAsync(entity);
            return Ok(entity);
        }

        [HttpDelete("{id}", Name = "DeleteToDo")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var existingToDo = await _repository.GetByIdAsync(userId, id);
            if (existingToDo == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(userId, id);
            return NoContent();
        }
    }
}
