using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo.Busines.Exceptions;
using Todo.Data.Models;
using Todo.Data.Models.Auth;
using Todo.Services.Constants;
using Todo.Services.Dtos;
using Todo.Services.Interfaces;

namespace Todo.Services.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signinManager;
    private readonly AuthConfiguration _authConfig;

    public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<AuthConfiguration> authConfig)
    {
        _userManager = userManager;
        _signinManager = signInManager;
        _authConfig = authConfig.Value;
    }

    public async Task Register(UserDto user)
    {
        var identityUser = new ApplicationUser()
        {
            UserName = user.Email,
            Email = user.Email,
        };

        var userAlreadyExists = await UserExists(identityUser.UserName);
        if (userAlreadyExists)
            throw new AuthenticationException($"A user with name {identityUser.UserName} already exists", ErrorCodes.Auth.DuplicateUser);

        var result = await _userManager.CreateAsync(identityUser, user.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException($"A problem occured while creating the user {identityUser.UserName}");
    }

    public async Task Login(UserDto user)
    {
        var userFromDb = await _userManager.FindByEmailAsync(user.Email)
            ?? throw new AuthenticationException($"The user does not exist", ErrorCodes.Auth.UserDoesNotExist);

        if (userFromDb.AccessFailedCount >= 3)
            await LockoutUserAccount(userFromDb);

        var signinResult = await _signinManager.PasswordSignInAsync(userFromDb, user.Password, isPersistent: false, lockoutOnFailure: false);

        if (!signinResult.Succeeded)
        {
            if (signinResult.IsLockedOut)
                throw new AuthenticationException("User is locked out", ErrorCodes.Auth.UserIsLockedOut);

            await _userManager.AccessFailedAsync(userFromDb);
            throw new AuthenticationException("The user was not able to sign in, increasing failed count", ErrorCodes.Auth.AuthenticationFailed);
        }
    }

    public Task Logout() => _signinManager.SignOutAsync();

    public async Task<string> GenerateJWTToken(string userName)
    {
        var userFromDb = await _userManager.FindByNameAsync(userName)
            ?? throw new AuthenticationException($"The user does not exist", ErrorCodes.Auth.UserDoesNotExist);

        return GenerateJWTTokenByApplicationUser(userFromDb);
    }

    private string GenerateJWTTokenByApplicationUser(ApplicationUser user)
    {
        if (user.UserName == null) throw new ArgumentNullException(nameof(user.UserName));

        try
        {
            var tokenExpiration = DateTime.UtcNow.AddMinutes(_authConfig.ExpirationInMinutes);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new([new(ClaimTypes.NameIdentifier, user.UserName)]),
                Audience = _authConfig.Audience,
                Issuer = _authConfig.Issuer,
                Expires = tokenExpiration,
                SigningCredentials = GetSigningCredentials()
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        catch
        {
            throw new Exception("An error ocurred while creating the token");
        }
    }

    private SigningCredentials GetSigningCredentials()
    {

        var key = Encoding.UTF8.GetBytes(_authConfig.Secret);
        return  new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature);
    }

    private async Task LockoutUserAccount(ApplicationUser user)
    {
        var lockoutResult = await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddMinutes(5));
        if (!lockoutResult.Succeeded)
            throw new InvalidOperationException("There was a problem while trying to lock the user account");
    }

    private async Task<bool> UserExists(string username)
    {
        var result = await _userManager.FindByEmailAsync(username);
        return result != null;
    }
}
