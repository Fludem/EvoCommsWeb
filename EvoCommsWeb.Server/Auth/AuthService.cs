using System.Security.Claims;
using EvoCommsWeb.Server.Auth.DTOs;
using EvoCommsWeb.Server.Requests.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EvoCommsWeb.Server.Auth;

public class AuthService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<AuthResult> RegisterUserAsync(RegisterRequest request)
    {
        var userExists = await _userManager.FindByNameAsync(request.Username);
        if (userExists != null)
            return new AuthResult { Success = false, Message = "User already exists!" };

        var user = new IdentityUser
        {
            UserName = request.Username,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return new AuthResult { Success = false, Message = "Invalid username or password.", Errors = result.Errors };

        await _signInManager.SignInAsync(user, isPersistent: false);
        return new AuthResult { Success = true, Message = "User registered successfully!" };
    }

    public async Task<AuthResult> LoginUserAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.Username);
        if (user == null)
            return new AuthResult{ Success = false, Message = "Invalid username or password."};

        var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
            return new AuthResult{ Success = true, Message = "Logged in successfully." };

        return result.IsLockedOut ? new AuthResult { Success = false, Message = "User account locked out."} : new AuthResult { Success = false, Message = "Invalid username or password." };
    }
    
    public async Task<AuthResult> LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult{ Success = true, Message = "Logged out successfully." };
    }

    public bool IsAuthenticatedAsync(ClaimsPrincipal user)
    {
        return user.Identity?.IsAuthenticated ?? false;
    }
}