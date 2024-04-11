using BuildPWR.TodoApp.Api.Data;
using BuildPWR.TodoApp.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildPWR.TodoApp.Api.Controllers
{
    [Route("api/todoitems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoAppDbContext _context;

        public TodoItemsController(TodoAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetCompletedTodoItems()
        {
            return await _context.TodoItems.Where(x => x.IsComplete)
                                           .ToListAsync();
        }

        [HttpGet("uncompleted")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetUnCompletedTodoItems()
        {
            return await _context.TodoItems.Where(x=> !x.IsComplete)
                                           .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id) {
                return BadRequest();
            }

            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null) {
                return NotFound();
            }

            todo.Title = todoItem.Title;
            todo.Description = todoItem.Description;
            todo.IsComplete = todoItem.IsComplete;

            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
