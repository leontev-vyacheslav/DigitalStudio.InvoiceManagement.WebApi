using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Services;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;

public sealed class DeleteInvoiceCommand : DatabaseCommand
{
    public DeleteInvoiceCommand(AppDataContext appDataContext) : base(appDataContext)
    {
    }

    public async Task<InvoiceDataModel?> DeleteAsync(Guid id)
    {
        var deletingInvoice = await AppDataContext.FindAsync<InvoiceDataModel>(id);

        if (deletingInvoice != null)
        {
            AppDataContext.Invoices.Remove(deletingInvoice);
            await AppDataContext.SaveChangesAsync();
        }

        return deletingInvoice;
    }
}