using Microsoft.EntityFrameworkCore;
using Todo.Busines.Exceptions;
using Todo.Data;
using Todo.Data.Interfaces;
using Todo.Data.Models;
using Todo.Services.Interfaces;

namespace Todo.Busines.Services;

public class TodoService : ITodoService
{
    private readonly ApplicationDbContext _database;

    public TodoService(ApplicationDbContext database) => _database = database;

    //public Task<List<TodoModel>> GetTodos()
    //{
    //    try
    //    {
    //        return _database.Todos.ToListAsync();
    //    }
    //    catch
    //    {
    //        throw new DatabaseException("Something when wrong when retriving the list of Todos from the database");
    //    }
    //}

    //public Task<TodoModel?> GetTodoById(int id)
    //{
    //    try
    //    {
    //        return _database.Todos.FirstOrDefaultAsync(x => x.Id == id);
    //    }
    //    catch
    //    {
    //        throw new DatabaseException($"Something when wrong when retriving Todos with ID {id} from the database");
    //    }
    //}

    //public async Task<bool> UpdateTodo(TodoModel todo)
    //{
    //    try
    //    {
    //        _database.Todos.Update(todo);
    //        var result = await _database.SaveChangesAsync();
    //        return result > 0;
    //    }
    //    catch
    //    {
    //        throw new DatabaseException($"Something when wrong while updating the Todo {todo.Id}");
    //    }
    //}

    //public async Task<bool> CreateTodo(TodoModel todo)
    //{
    //    // TODO: Change this
    //    try
    //    {
    //        _database.Todos.Update(todo);
    //        var result = await _database.SaveChangesAsync();
    //        return result > 0;
    //    }
    //    catch
    //    {
    //        throw new DatabaseException($"Something when wrong when retriving Todos with ID {todo.Id} from the database");
    //    }
    //}
}
