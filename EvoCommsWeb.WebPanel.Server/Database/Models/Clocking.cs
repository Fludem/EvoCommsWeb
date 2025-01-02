// ReSharper disable ClassNeverInstantiated.Global

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvoCommsWeb.WebPanel.Server.Database.Models;

public class Clocking
{
    [Key] public int Id { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime ClockedAt { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime ReceivedAt { get; set; }

    [Required] public int EmployeeId { get; set; }

    [ForeignKey("EmployeeId")] public required Employee Employee { get; set; }

    [Required] public int TerminalId { get; set; }

    [ForeignKey("TerminalId")] public required Terminal Terminal { get; set; }
}