﻿using System.ComponentModel.DataAnnotations;

namespace Todo.Services.Dtos;

public class UserDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}