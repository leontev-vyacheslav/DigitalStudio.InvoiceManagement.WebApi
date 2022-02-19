using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Models.Paging;
using DigitalStudio.InvoiceManagement.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;

public sealed class GetInvoiceListCommand : DatabaseCommand
{
    public GetInvoiceListCommand(AppDataContext appDataContext) : base(appDataContext)
    {
    }

    public async Task<IEnumerable<InvoiceDataModel>> GetAsync(PageInfo pageInfo)
    {
        return await AppDataContext
            .Invoices
            .Include(i => i.PaymentWay)
            .Include(i => i.ProcessingStatus)
            .Skip(pageInfo.Size * (pageInfo.No - 1))
            .Take(pageInfo.Size)
            .ToListAsync();
    }
}