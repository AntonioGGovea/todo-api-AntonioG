using Todo.Data.Models;
using Todo.Services.Dtos;

namespace Todo.unit.Tests.Fixtures;

internal class TodoData
{
    public TodoModel Todo1 => new()
    {
        Id = 1,
        Title = nameof(Todo1),
        IsDone = true,
    };

    public TodoModel Todo2 => new()
    {
        Id = 2,
        Title = nameof(Todo2),
        IsDone = false,
    };

    public TodoCreateDto TodoCreateDto => new()
    {
        Title = nameof(TodoCreateDto),
        IsDone = false,
    };

    public TodoModel TodoUpdateForTodo1 => new()
    {
        Id = Todo1.Id,
        Title = nameof(TodoUpdateForTodo1),
        IsDone = Todo1.IsDone,
    };

    public List<TodoModel> TodoList => new() { Todo1, Todo2 };
}
