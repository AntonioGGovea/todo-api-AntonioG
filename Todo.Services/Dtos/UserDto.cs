using System.ComponentModel.DataAnnotations;

namespace Todo.Services.Dtos;

public class UserDto
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
