using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Busines.Exceptions;
using Todo.Services.Dtos;
using Todo.Services.Interfaces;

namespace Todo.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[Controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] UserDto user)
    {
        try
        {
            await _authService.Register(user);
            return Created();
        }
        catch (AuthenticationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Login([FromBody] UserDto user)
    {
        try
        {
            await _authService.Login(user);
            var token = await _authService.GenerateJWTToken(user.Email);
            return Ok(token);
        }
        catch (AuthenticationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await _authService.Logout();
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet("generateToken")]
    public async Task<ActionResult<string>> GetGenerateToken()
    {
        try
        {
            if (string.IsNullOrEmpty(User.Identity?.Name))
                return NotFound("User Not found.");

            var token = await _authService.GenerateJWTToken(User.Identity.Name);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
