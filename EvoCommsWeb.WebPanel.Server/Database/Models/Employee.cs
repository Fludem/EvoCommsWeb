// ReSharper disable ClassNeverInstantiated.Global

using System.ComponentModel.DataAnnotations;

namespace EvoCommsWeb.WebPanel.Server.Database.Models;

public class Employee
{
    [Key] public int Id { get; set; }

    [Required] public int ClockingId { get; set; }

    [Required] [StringLength(100)] public required string FullName { get; set; }

    public ICollection<Clocking> Clockings { get; set; } = new List<Clocking>();
}