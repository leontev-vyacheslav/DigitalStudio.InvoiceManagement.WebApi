using DigitalStudio.InvoiceManagement.WebApi.Commands.Dictionary;
using DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;

namespace DigitalStudio.InvoiceManagement.WebApi;

public static class Extensions
{
    public static IServiceCollection AddAppCommands(this IServiceCollection services)
    {
        return services
            .AddTransient<GetInvoiceCommand>()
            .AddTransient<GetInvoiceListCommand>()
            .AddTransient<PostInvoiceCommand>()
            .AddTransient<DeleteInvoiceCommand>()
            .AddTransient<GetDictionaryCommand>();
    }
}