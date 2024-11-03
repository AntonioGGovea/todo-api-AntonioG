using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo;
using Todo.Data;

namespace Todo.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Todoes
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TodoModel>>> GetTodo()
        //{
        //    return await _context.Todo.ToListAsync();
        //}

        //// GET: api/Todoes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<TodoModel>> GetTodo(int id)
        //{
        //    var todo = await _context.Todo.FindAsync(id);

        //    if (todo == null)
        //    {
        //        return NotFound();
        //    }

        //    return todo;
        //}

        //// PUT: api/Todoes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTodo(int id, TodoModel todo)
        //{
        //    if (id != todo.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(todo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TodoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Todoes
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TodoModel>> PostTodo(TodoModel todo)
        //{
        //    _context.Todo.Add(todo);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        //}

        //// DELETE: api/Todoes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTodo(int id)
        //{
        //    var todo = await _context.Todo.FindAsync(id);
        //    if (todo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Todo.Remove(todo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TodoExists(int id)
        //{
        //    return _context.Todo.Any(e => e.Id == id);
        //}
    }
}
