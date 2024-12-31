namespace EvoCommsWeb.Server.Terminals.ZK.DTOs;

public class ZkClocking
{
    public required string UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Status { get; set; }
    public string? VerifyMode { get; set; }
    public string? WorkCode { get; set; }
}