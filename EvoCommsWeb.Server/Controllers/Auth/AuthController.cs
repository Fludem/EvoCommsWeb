using EvoCommsWeb.Server.Auth;
using EvoCommsWeb.Server.Auth.Responses;
using EvoCommsWeb.Server.Requests.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EvoCommsWeb.Server.Controllers.Auth;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(AuthService authService) : ControllerBase
{
    /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerRequest">Registration details.</param>
        /// <returns>Success or failure response.</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await authService.RegisterUserAsync(registerRequest);
            if (!result.Success)
                return BadRequest(result);
    ///     Registers a new user.
    /// </summary>
    /// <param name="registerRequest">Registration details.</param>
    /// <returns>Success or failure response.</returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        AuthResult result = await authService.RegisterUserAsync(registerRequest);
        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

        /// <summary>
        /// Login endpoint using ASP.NET Core Identity.
        /// </summary>
        /// <param name="loginRequest">Login credentials.</param>
        /// <returns>Success or failure response.</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await authService.LoginUserAsync(loginRequest);
            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }

        /// <summary>
        /// Logout endpoint.
        /// </summary>
        /// <returns>Success response.</returns>
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            var result = await authService.LogoutUserAsync();
            return Ok(result);
        }

        /// <summary>
        /// Endpoint to check if the user is authenticated.
        /// </summary>
        /// <returns>Authentication status.</returns>
        [HttpGet("isauthenticated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult IsAuthenticated()
        {
            var isAuthenticated = authService.IsAuthenticatedAsync(User);
            return Ok(new { isAuthenticated });
        }
}
