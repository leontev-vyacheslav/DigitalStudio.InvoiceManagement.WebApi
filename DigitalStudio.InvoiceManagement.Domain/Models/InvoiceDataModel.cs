using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalStudio.InvoiceManagement.Domain.Contracts;

namespace DigitalStudio.InvoiceManagement.Domain.Models;

public class InvoiceDataModel : IEntity<Guid>
{
    [Key]
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ChangeDate { get; set; }

    public int ProcessingStatusId { get; set; }

    public int PaymentWayId { get; set; }

    public decimal Amount { get; set; }

    [ForeignKey("ProcessingStatusId")]
    public virtual ProcessingStatusDataModel? ProcessingStatus { get; set; }

    [ForeignKey("PaymentWayId")]
    public virtual PaymentWayDataModel? PaymentWay { get; set; }
}