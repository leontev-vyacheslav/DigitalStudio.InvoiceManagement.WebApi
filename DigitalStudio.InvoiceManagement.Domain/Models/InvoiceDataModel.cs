using DigitalStudio.InvoiceManagement.Domain.Contracts;

namespace DigitalStudio.InvoiceManagement.Domain.Models;

public class InvoiceDataModel : IEntity<Guid>
{
    public Guid Id { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ChangeDate { get; set; }

    public int ProcessingStatusId { get; set; }

    public int PaymentWayId { get; set; }

    public decimal Amount { get; set; }
}