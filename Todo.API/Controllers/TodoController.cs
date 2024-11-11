using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Models;
using Todo.Services.Dtos;
using Todo.Services.Interfaces;

namespace Todo.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[Controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService) =>
        _todoService = todoService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodo(int id)
    {
        var todo = await _todoService.GetTodoById(id);

        return todo == null
            ? NotFound()
            : Ok(todo);
    }

    [HttpGet]
    public Task<List<TodoModel>> GetAll() =>
        _todoService.GetTodos();

    [HttpPost]
    public async Task<ActionResult<TodoModel>> CreateTodo([FromBody] TodoCreateDto model)
    {
        return await _todoService.CreateTodo(model);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateTodo([FromBody] TodoModel todo)
    {
        var todoEntity = await _todoService.GetTodoById(todo.Id);

        if (todoEntity == null)
            return NotFound();

        await _todoService.UpdateTodo(todo);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todoEntity = await _todoService.GetTodoById(id);

        if (todoEntity == null)
            return NotFound();

        await _todoService.DeleteTodo(todoEntity);
        return Ok();
    }
}
