using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.API.Controllers;
using Todo.Data.Models;
using Todo.Services.Interfaces;
using Todo.unit.Tests.Fixtures;

namespace Todo.unit.Tests;

public class TodoControllerTests
{
    private readonly Mock<ITodoService> _todoService;
    private readonly TodoController _todoController;
    private readonly TodoData _todoData;

    public TodoControllerTests()
    {
        _todoService = new Mock<ITodoService>();
        _todoController = new TodoController(_todoService.Object);
        _todoData = new TodoData();
    }

    [Fact]
    public async Task GetAll_ReturnsTodoList()
    {
        // Arrange
        var todoList = _todoData.TodoList;
        _todoService.Setup(x => x.GetTodos()).ReturnsAsync(todoList);

        // Act
        var result = (await _todoController.GetAll()).Result as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.Equal(todoList, result.Value);
    }

    [Fact]
    public async Task GetTodoById_ReturnsTodo()
    {
        // Arrange
        var todo = _todoData.Todo1;
        _todoService.Setup(x => x.GetTodoById(todo.Id)).ReturnsAsync(todo);

        // Act
        var result = (await _todoController.GetTodo(todo.Id)).Result as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        Assert.Equal(todo, result.Value);
    }

    [Fact]
    public async Task GetTodoById_WithUnexistingTodo_ReturnsNotFound()
    {
        // Arrange
        var todoId = 1;
        _todoService.Setup(x => x.GetTodoById(todoId)).Returns(Task.FromResult<TodoModel?>(null));

        // Act
        var result = (await _todoController.GetTodo(todoId)).Result as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
    }

    [Fact]
    public async Task CreateTodo_WithValidData_ReturnsOk()
    {
        // Arrange
        var todoToCreate = _todoData.TodoCreateDto;
        var todoCreated = _todoData.Todo1;
        _todoService.Setup(x => x.CreateTodo(todoToCreate)).ReturnsAsync(todoCreated);

        // Act
        var result = (await _todoController.CreateTodo(todoToCreate)).Result as ObjectResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        Assert.Equal(todoCreated, result.Value);
    }

    [Fact]
    public async Task UpdateTodo_WithValidData_ReturnsOk()
    {
        // Arrange
        var todo = _todoData.TodoUpdateForTodo1;
        _todoService.Setup(x => x.GetTodoById(todo.Id)).ReturnsAsync(_todoData.Todo1);
        _todoService.Setup(x => x.UpdateTodo(todo)).Returns(Task.CompletedTask);

        // Act
        var result = await _todoController.UpdateTodo(todo) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }

    [Fact]
    public async Task UpdateTodo_WithUnexistingTodo_ReturnsNotFound()
    {
        // Arrange
        var todo = _todoData.Todo1;
        _todoService.Setup(x => x.UpdateTodo(todo));

        // Act
        var result = await _todoController.UpdateTodo(todo) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
    }

    [Fact]
    public async Task DeleteTodo_WithValidData_ReturnsOk()
    {
        // Arrange
        var todo = _todoData.TodoUpdateForTodo1;
        _todoService.Setup(x => x.GetTodoById(todo.Id)).ReturnsAsync(todo);
        _todoService.Setup(x => x.DeleteTodo(todo)).Returns(Task.CompletedTask);

        // Act
        var result = await _todoController.DeleteTodo(todo.Id) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }

    [Fact]
    public async Task DeleteTodo_WithUnexistingTodo_ReturnsNotFound()
    {
        // Arrange
        var todo = _todoData.Todo1;
        _todoService.Setup(x => x.DeleteTodo(todo));

        // Act
        var result = await _todoController.DeleteTodo(todo.Id) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
    }
}