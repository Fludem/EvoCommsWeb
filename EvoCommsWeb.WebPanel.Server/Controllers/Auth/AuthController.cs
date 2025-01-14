using EvoCommsWeb.WebPanel.Server.Auth;
using EvoCommsWeb.WebPanel.Server.Auth.Responses;
using EvoCommsWeb.WebPanel.Server.Requests.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EvoCommsWeb.WebPanel.Server.Controllers.Auth;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    /// <summary>
    ///     Register a new user.
    /// </summary>
    /// <param name="registerRequest">Registration details.</param>
    /// <returns>Success or failure response.</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
            return HandleBadRequest(ModelState);
        AuthResult result = await authService.RegisterUserAsync(registerRequest);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    ///     Endpoint to login and receive Session Cookie.
    /// </summary>
    /// <param name="loginRequest">Login credentials.</param>
    /// <returns>Success or failure response.</returns>
    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResult>> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
            return HandleBadRequest(ModelState);
        AuthResult result = await authService.LoginUserAsync(loginRequest);
        if (!result.Success)
            return Unauthorized(result);

        return Ok(result);
    }

    /// <summary>
    ///     Generates a BadRequest response with a standardized AuthResult object.
    /// </summary>
    /// <param name="modelState">Collection of error messages.</param>
    /// <returns>A BadRequest response containing an AuthResult.</returns>
    private ActionResult<AuthResult> HandleBadRequest(ModelStateDictionary modelState)
    {
        IEnumerable<string> errorMessages = modelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage);
        AuthResult authResult = new()
        {
            Success = false,
            Message = "Invalid Request. Validation failed.",
            Errors = errorMessages.Select(error => new IdentityError { Description = error })
        };
        return BadRequest(authResult);
    }


    /// <summary>
    ///     Logout endpoint to invalidate session.
    /// </summary>
    /// <returns>Success response.</returns>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthResult>> Logout()
    {
        AuthResult result = await authService.LogoutUserAsync();
        return Ok(result);
    }

    /// <summary>
    ///     Check if the user is authenticated.
    /// </summary>
    /// <returns>Authentication status.</returns>
    [HttpGet("isauthenticated")]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(AuthResult), StatusCodes.Status401Unauthorized)]
    public IActionResult IsAuthenticated()
    {
        AuthResult isAuthenticated = authService.IsAuthenticatedAsync(User);
        return isAuthenticated.Success ? Ok(isAuthenticated) : Unauthorized(isAuthenticated);
    }
}