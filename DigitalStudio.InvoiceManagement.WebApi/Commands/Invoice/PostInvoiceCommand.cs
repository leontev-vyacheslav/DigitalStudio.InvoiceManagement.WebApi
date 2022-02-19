using AutoMapper;
using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Models.Views;
using DigitalStudio.InvoiceManagement.WebApi.Services;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;

public sealed class PostInvoiceCommand : DatabaseCommand
{
    private readonly IMapper _mapper;

    public PostInvoiceCommand(AppDataContext appDataContext, IMapper mapper) : base(appDataContext)
    {
        _mapper = mapper;
    }

    public async Task<InvoiceDataModel> PostAsync(InvoiceModel model)
    {
        var editInvoice = _mapper.Map<InvoiceDataModel>(model);

        var entityEntry = model.Id == null
            ? await AppDataContext.Invoices.AddAsync(editInvoice)
            : AppDataContext.Invoices.Update(editInvoice);

        await AppDataContext.SaveChangesAsync();

        return entityEntry.Entity;
    }
}