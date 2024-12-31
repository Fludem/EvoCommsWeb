using Microsoft.AspNetCore.Identity;

namespace EvoCommsWeb.Server.Auth.DTOs;

public class AuthResult
{
    public bool Success { get; set; }
    public required string Message { get; set; }
    public IEnumerable<IdentityError>? Errors { get; set; }
}