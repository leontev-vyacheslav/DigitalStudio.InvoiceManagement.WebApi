using AutoMapper;
using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Models.Views;
using DigitalStudio.InvoiceManagement.WebApi.Services;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;

public sealed class PostInvoiceCommand
{
    private readonly AppDataContext _appDataContext;

    private readonly IMapper _mapper;

    public PostInvoiceCommand(AppDataContext appDataContext, IMapper mapper)
    {
        _appDataContext = appDataContext;
        _mapper = mapper;
    }

    public async Task<InvoiceDataModel> PostAsync(InvoiceModel model)
    {
        var editInvoice = _mapper.Map<InvoiceDataModel>(model);

        var entityEntry = model.Id == null
            ? await _appDataContext.Invoices.AddAsync(editInvoice)
            : _appDataContext.Invoices.Update(editInvoice);

        await _appDataContext.SaveChangesAsync();

        return entityEntry.Entity;
    }
}