namespace EvoCommsWeb.Server.Terminals.ZK.Responses;

public class ZkResponse
{
    public bool Success { get; set; }
    public required string Message { get; set; }
    public int? ErrorCode { get; set; }
}