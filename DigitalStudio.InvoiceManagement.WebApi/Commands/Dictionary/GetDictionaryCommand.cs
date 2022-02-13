using DigitalStudio.InvoiceManagement.Domain.Contracts;
using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Dictionary;

public sealed class GetDictionaryCommand
{
    private readonly AppDataContext _appDataContext;

    public GetDictionaryCommand(AppDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    public async Task<IEnumerable<DictionaryBaseModel>> GetAsync(string name)
    {
        var dictionary = $"{name}DataModel" switch
        {
            nameof(PaymentWayDataModel) => await _appDataContext.PaymentWays.ToListAsync(),
            nameof(ProcessingStatusDataModel) => await _appDataContext.ProcessingStatuses.ToListAsync(),
            _ => null as IEnumerable<DictionaryBaseModel>
        };

        return dictionary;
    }
}