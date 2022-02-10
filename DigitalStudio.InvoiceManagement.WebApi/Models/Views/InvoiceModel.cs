using System.ComponentModel.DataAnnotations;

namespace DigitalStudio.InvoiceManagement.WebApi.Models.Views;

public sealed class InvoiceModel : IValidatableObject
{
    public Guid? Id { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }

    public DateTime ChangeDate { get; set; }

    [Range(1, 3)]
    [Required]
    public int ProcessingStatusId { get; set; }

    [Required]
    [Range(1, 3)]
    public int PaymentWayId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CreationDate > ChangeDate)
        {
            yield return new ValidationResult("CreationDate must be greater than ChangeDate.", new[] { nameof(CreationDate), nameof(ChangeDate) });
        }

        if (Amount < 0.0m)
        {
            yield return new ValidationResult("Amount must be greater than 0.", new[] { nameof(Amount) });
        }
    }
}