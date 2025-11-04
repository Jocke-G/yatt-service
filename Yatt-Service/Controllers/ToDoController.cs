using Microsoft.AspNetCore.Mvc;
using Yatt_Service.Contracts;
using Yatt_Service.Mapping;
using Yatt_Service.RepositoryInterfaces;

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
            if (toDoItem == null)
            {
                return BadRequest("ToDo item cannot be null.");
            }
            var entity = toDoItem.ToEntity();
            var createdToDo = await _repository.AddAsync(entity);
            return CreatedAtRoute("GetToDoById", new { id = createdToDo.Id }, createdToDo.ToContract());
        }

        [HttpGet(Name = "ReadToDos")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ToDoItemContract>), StatusCodes.Status200OK)]
        public async Task<IEnumerable<ToDoItemContract>> Get()
        {
            var todos = await _repository.GetAllAsync();
            return todos.ToContracts();
        }

        [HttpGet("{id}", Name = "ReadToDoById")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ToDoItemContract), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
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
        public async Task<IActionResult> Put(int id, [FromBody] ToDoItemContract contract)
        {
            var existing = await _repository.GetByIdAsync(id);
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
        public async Task<IActionResult> Delete(int id)
        {
            var existingToDo = await _repository.GetByIdAsync(id);
            if (existingToDo == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
