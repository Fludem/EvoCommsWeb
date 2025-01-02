using System.ComponentModel.DataAnnotations;

namespace EvoCommsWeb.WebPanel.Server.Terminals.ZK.Requests;

public class ZkAttendanceDataRequest
{
    /// <summary>
    ///     Device Serial Number
    /// </summary>
    [Required]
    public required string SN { get; set; }

    /// <summary>
    ///     Table name, typically 'ATTLOG'
    /// </summary>
    [Required]
    public required string Table { get; set; }

    /// <summary>
    ///     Stamp number for synchronization
    /// </summary>
    public required string Stamp { get; set; }
}