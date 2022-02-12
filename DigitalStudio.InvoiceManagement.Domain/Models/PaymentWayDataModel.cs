using System.ComponentModel.DataAnnotations.Schema;
using DigitalStudio.InvoiceManagement.Domain.Contracts;

namespace DigitalStudio.InvoiceManagement.Domain.Models;

public class PaymentWayDataModel : DictionaryBaseModel
{
    [NotMapped]
    [InverseProperty("PaymentWay")]
    public virtual ICollection<InvoiceDataModel>? Invoices { get; set; }
}