using System.ComponentModel.DataAnnotations;

namespace Todo.Data.Models;

public class TodoModel
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public bool IsDone { get; set; }
}
