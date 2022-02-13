using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Models.Paging;
using DigitalStudio.InvoiceManagement.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;

public sealed class GetInvoiceListCommand
{
    private readonly AppDataContext _appDataContext;

    public GetInvoiceListCommand(AppDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async Task<IEnumerable<InvoiceDataModel>> GetAsync(PageInfo pageInfo)
    {
        return await _appDataContext
            .Invoices
            .Include(i=> i.PaymentWay)
            .Include(i => i.ProcessingStatus)
            .Skip(pageInfo.Size * (pageInfo.No - 1))
            .Take(pageInfo.Size)
            .ToListAsync();
    }
}