using System.ComponentModel.DataAnnotations;

namespace DigitalStudio.InvoiceManagement.WebApi.Models.Paging;

public sealed class PageInfo
{
    [Required]
    [Range(1, int.MaxValue)]
    public int No { get; set; }

    [Required]
    [Range(1, 100)]
    public int Size { get; set; }
}