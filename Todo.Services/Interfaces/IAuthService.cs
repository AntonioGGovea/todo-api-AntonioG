using Microsoft.AspNetCore.Identity;
using Todo.Services.Dtos;

namespace Todo.Services.Interfaces;

public interface IAuthService
{
    Task Register(UserDto user);
    Task Login(UserDto user);
    Task Logout();
    Task<string> GenerateJWTToken(string userName);
}
