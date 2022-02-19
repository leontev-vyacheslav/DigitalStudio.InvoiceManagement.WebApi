using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;

public sealed class GetInvoiceCommand : DatabaseCommand
{
    public GetInvoiceCommand(AppDataContext appDataContext) : base(appDataContext)
    {
    }

    public async Task<InvoiceDataModel> GetAsync(Guid id)
    {
        return await AppDataContext
            .Invoices.FirstOrDefaultAsync(i => i.Id == id);
    }
}