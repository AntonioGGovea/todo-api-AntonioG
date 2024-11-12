using System.ComponentModel.DataAnnotations;

namespace Todo.Services.Dtos;

public class TodoCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    public bool IsDone { get; set; }
}
