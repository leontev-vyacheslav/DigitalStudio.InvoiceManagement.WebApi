using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Services;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;

public sealed class DeleteInvoiceCommand
{
    private readonly AppDataContext _appDataContext;

    public DeleteInvoiceCommand(AppDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async Task<InvoiceDataModel> DeleteAsync(Guid id)
    {
        var deletingInvoice = await _appDataContext.FindAsync<InvoiceDataModel>(id);

        if (deletingInvoice != null)
        {
            _appDataContext.Invoices.Remove(deletingInvoice);
            await _appDataContext.SaveChangesAsync();
        }

        return deletingInvoice;
    }
}