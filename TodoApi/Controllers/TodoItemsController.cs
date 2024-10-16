using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/TodoItems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemModel>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemModel>> GetTodoItemModel(long id)
        {
            var todoItemModel = await _context.TodoItems.FindAsync(id);

            if (todoItemModel == null)
            {
                return NotFound();
            }

            return todoItemModel;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItemModel(long id, TodoItemModel todoItemModel)
        {
            if (id != todoItemModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItemModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemModel>> PostTodoItemModel(TodoItemModel todoItemModel)
        {
            _context.TodoItems.Add(todoItemModel);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItemModel", new { id = todoItemModel.Id }, todoItemModel);
            return CreatedAtAction(nameof(GetTodoItemModel), new { id = todoItemModel.Id }, todoItemModel);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItemModel(long id)
        {
            var todoItemModel = await _context.TodoItems.FindAsync(id);
            if (todoItemModel == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItemModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemModelExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
