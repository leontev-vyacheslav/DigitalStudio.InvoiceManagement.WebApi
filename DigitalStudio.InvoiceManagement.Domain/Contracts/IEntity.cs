namespace DigitalStudio.InvoiceManagement.Domain.Contracts;

public interface IEntity<T>
{
    T Id { get; set; }
}