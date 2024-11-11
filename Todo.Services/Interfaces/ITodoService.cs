using Todo.Data.Models;
using Todo.Services.Dtos;

namespace Todo.Services.Interfaces;

public interface ITodoService
{
    Task<List<TodoModel>> GetTodos();
    Task<TodoModel?> GetTodoById(int id);
    Task<bool> UpdateTodo(TodoModel todo);
    Task<TodoModel> CreateTodo(TodoCreateDto todo);
    Task DeleteTodo(TodoModel todo);
}
