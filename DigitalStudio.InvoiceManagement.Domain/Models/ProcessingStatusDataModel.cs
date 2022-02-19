using System.ComponentModel.DataAnnotations.Schema;
using DigitalStudio.InvoiceManagement.Domain.Contracts;

namespace DigitalStudio.InvoiceManagement.Domain.Models;

public class ProcessingStatusDataModel : DictionaryBaseDataModel
{
    [NotMapped]
    [InverseProperty("ProcessingStatus")]
    public virtual ICollection<InvoiceDataModel>? Invoices { get; set; }
}