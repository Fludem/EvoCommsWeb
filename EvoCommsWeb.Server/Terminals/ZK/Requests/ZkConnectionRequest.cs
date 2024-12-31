using System.ComponentModel.DataAnnotations;

namespace EvoCommsWeb.Server.Terminals.ZK.Requests;

public class ZkConnectionRequest
{
    /// <summary>
    /// Device Serial Number
    /// </summary>
    [Required]
    public required string SN { get; set; }
    
    /// <summary>
    /// Options parameter, typically 'all'
    /// </summary>
    public required string Options { get; set; }
    
    /// <summary>
    /// Push version number
    /// </summary>
    public required string PushVer { get; set; }
    
    /// <summary>
    /// Language code
    /// </summary>
    public required string Language { get; set; }
}
