using Microsoft.EntityFrameworkCore;
using Todo.Busines.Exceptions;
using Todo.Data;
using Todo.Data.Interfaces;
using Todo.Data.Models;
using Todo.Services.Dtos;
using Todo.Services.Interfaces;

namespace Todo.Busines.Services;

public class TodoService : ITodoService
{
    private readonly IApplicationDbContext _database;

    public TodoService(IApplicationDbContext database) => _database = database;

    public Task<List<TodoModel>> GetTodos()
    {
        try
        {
            return _database.Todo.ToListAsync();
        }
        catch
        {
            throw new DatabaseException("Something when wrong when retriving the list of Todos from the database");
        }
    }

    public Task<TodoModel?> GetTodoById(int id)
    {
        try
        {
            return _database.Todo.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            throw new DatabaseException($"Something when wrong when retriving Todos with ID {id} from the database");
        }
    }

    public async Task<bool> UpdateTodo(TodoModel todo)
    {
        try
        {
            _database.Todo.Update(todo);
            var result = await _database.SaveChangesAsync();
            return result > 0;
        }
        catch(Exception ex)
        {
            throw new DatabaseException($"Something when wrong while updating the Todo {todo.Id}");
        }
    }

    public async Task<TodoModel> CreateTodo(TodoCreateDto todo)
    {
        try
        {
            var todoEntity = new TodoModel()
            {
                IsDone = todo.IsDone,
                Title = todo.Title,
            };
            _database.Todo.Add(todoEntity);
            await _database.SaveChangesAsync();
            return todoEntity;
        }
        catch
        {
            throw new DatabaseException("Something when wrong when creating the Todo");
        }
    }

    public async Task DeleteTodo(TodoModel todo)
    {
        try
        {
            _database.Todo.Remove(todo);
            await _database.SaveChangesAsync();
        }
        catch
        {
            throw new DatabaseException($"Something when wrong when deleting the Todo {todo.Id}");
        }
    }
}
