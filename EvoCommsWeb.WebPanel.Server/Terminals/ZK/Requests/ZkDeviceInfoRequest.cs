using System.ComponentModel.DataAnnotations;

namespace EvoCommsWeb.WebPanel.Server.Terminals.ZK.Requests;

public class ZkDeviceInfoRequest
{
    /// <summary>
    ///     Device Serial Number
    /// </summary>
    [Required]
    public required string SN { get; set; }

    /// <summary>
    ///     Device information string containing version, users, fingerprints etc.
    ///     Format: Ver,Users,Fingerprints,Attendance,IP,FPAlgo,FaceAlgo,FaceEnroll,EnrolledFaces,Features
    /// </summary>
    [Required]
    public required string INFO { get; set; }
}