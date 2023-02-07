using DigitalStudio.InvoiceManagement.Domain.Contracts;
using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands.Dictionary;

public sealed class GetDictionaryCommand : DatabaseCommand
{
    public GetDictionaryCommand(AppDataContext appDataContext) : base(appDataContext)
    {
    }

    public async Task<IEnumerable<DictionaryBaseDataModel>?> GetAsync(string name)
    {
        var dictionary = $"{name}DataModel" switch
        {
            nameof(PaymentWayDataModel) => await AppDataContext.PaymentWays.ToListAsync(),
            nameof(ProcessingStatusDataModel) => await AppDataContext.ProcessingStatuses.ToListAsync(),
            _ => null as IEnumerable<DictionaryBaseDataModel>
        };

        return dictionary;
    }
}