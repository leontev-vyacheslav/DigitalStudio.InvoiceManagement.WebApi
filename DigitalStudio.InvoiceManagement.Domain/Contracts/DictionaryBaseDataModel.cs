namespace DigitalStudio.InvoiceManagement.Domain.Contracts;

public abstract class DictionaryBaseDataModel : IEntity<int>
{
    public int Id { get; set; }

    public string? Name { get; set; }
}