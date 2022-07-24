using Microsoft.AspNetCore.Mvc;
using TodoBackend.Models;
using TodoBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private TodoService _todoService;
        public TodosController(TodoService todoService)
        {
            this._todoService = todoService;
        }
        // GET: api/<TodosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _todoService.Get());
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var todo = await _todoService.Get(id);

            if (todo == null)
                return NotFound();

            return Ok(todo);
        }

        // POST api/<TodosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Todo todo)
        {
            await _todoService.Create(todo);
            return Ok("Task Created Successfully");
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Todo todo)
        {
            var todoFromDB = await _todoService.Get(id);
            if (todoFromDB == null)
                return NotFound();

            await _todoService.Update(id, todo);
            return Ok("Task updated successfully.");
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var todo = await _todoService.Get(id);

            if(todo == null)
                return NotFound();
            
            await _todoService.Remove(id);
            return Ok("Task deleted successfully.");
        }
    }
}
