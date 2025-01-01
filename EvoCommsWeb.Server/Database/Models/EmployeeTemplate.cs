// ReSharper disable ClassNeverInstantiated.Global

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvoCommsWeb.Server.Database.Models;

public class EmployeeTemplate
{
    [Key] public int Id { get; set; }

    [Required] public required string TemplateData { get; set; }

    [Required] public DeviceModel DeviceModel { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime DateDownloaded { get; set; }

    // Foreign key to Terminal
    public int? ReceivedFromId { get; set; }

    [ForeignKey("ReceivedFromId")] public Terminal? ReceivedFrom { get; set; }
}