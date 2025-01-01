// ReSharper disable ClassNeverInstantiated.Global

using System.ComponentModel.DataAnnotations;

namespace EvoCommsWeb.Server.Database.Models;

public class Terminal
{
    [Key] public int Id { get; set; }

    [Required] [StringLength(50)] public required string SerialNumber { get; set; }

    [Required] public DeviceModel DeviceModel { get; set; }

    public DateTime? LastConnected { get; set; }

    // Navigation properties
    public ICollection<EmployeeTemplate> EmployeeTemplates { get; set; } = new List<EmployeeTemplate>();
    public ICollection<Clocking> Clockings { get; set; } = new List<Clocking>();
}