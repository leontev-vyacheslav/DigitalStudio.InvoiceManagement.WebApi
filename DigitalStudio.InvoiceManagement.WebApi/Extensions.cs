using System.Reflection;
using DigitalStudio.InvoiceManagement.WebApi.Commands;

namespace DigitalStudio.InvoiceManagement.WebApi;

public static class Extensions
{
    public static IServiceCollection AddAppCommands(this IServiceCollection services)
    {
        Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => !t.IsAbstract && typeof(DatabaseCommand).IsAssignableFrom(t))
            .ToList()
            .ForEach(t =>
            {
                services.AddTransient(t);
            });

        return services;
    }
}