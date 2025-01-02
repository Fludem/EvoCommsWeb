using Microsoft.AspNetCore.Identity;

namespace EvoCommsWeb.WebPanel.Server.Auth.Responses;

public class AuthResult
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public IEnumerable<IdentityError>? Errors { get; init; }
}