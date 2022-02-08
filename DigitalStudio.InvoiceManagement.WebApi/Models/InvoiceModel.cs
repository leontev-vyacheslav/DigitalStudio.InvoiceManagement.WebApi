namespace DigitalStudio.InvoiceManagement.WebApi.Models;

public class InvoiceModel
{
    public int Id { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ChangeDate { get; set; }

    public int ProcessingStatusId { get; set; }

    public int PaymentWayId { get; set; }

    public decimal Amount { get; set; }
}