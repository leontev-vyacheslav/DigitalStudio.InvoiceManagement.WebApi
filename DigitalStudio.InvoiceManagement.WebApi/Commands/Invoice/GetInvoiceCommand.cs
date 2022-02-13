using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;

public sealed class GetInvoiceCommand
{
    private readonly AppDataContext _appDataContext;

    public GetInvoiceCommand(AppDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async Task<InvoiceDataModel> GetAsync(Guid id)
    {
        return await _appDataContext
            .Invoices.FirstOrDefaultAsync(i => i.Id == id);
    }
}