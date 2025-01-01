using Microsoft.AspNetCore.Identity;

namespace EvoCommsWeb.Server.Auth.Responses;

public class AuthResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<IdentityError> Errors { get; set; }
}