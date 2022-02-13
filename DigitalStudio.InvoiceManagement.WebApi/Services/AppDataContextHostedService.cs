using DigitalStudio.InvoiceManagement.Domain.Models;

namespace DigitalStudio.InvoiceManagement.WebApi.Services;

public sealed class AppDataContextHostedService: IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AppDataContextHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var appDataContext = scope.ServiceProvider.GetRequiredService<AppDataContext>();
        var persistentStorageService = scope.ServiceProvider.GetRequiredService<PersistentStorageService>();

        var entityCollections = (await persistentStorageService.LoadAsync(cancellationToken)).ToList();

        await appDataContext.Invoices.AddRangeAsync(entityCollections
            .First(e => e.GetType().GetGenericArguments().Contains(typeof(InvoiceDataModel)))
            .Cast<InvoiceDataModel>(), cancellationToken);

        await appDataContext.PaymentWays.AddRangeAsync(entityCollections
            .First(e => e.GetType().GetGenericArguments().Contains(typeof(PaymentWayDataModel)))
            .Cast<PaymentWayDataModel>(), cancellationToken);

        await appDataContext.ProcessingStatuses.AddRangeAsync(entityCollections
            .First(e => e.GetType().GetGenericArguments().Contains(typeof(ProcessingStatusDataModel)))
            .Cast<ProcessingStatusDataModel>(), cancellationToken);

        await appDataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var appDataContext = scope.ServiceProvider.GetRequiredService<AppDataContext>();

        await appDataContext.SaveChangesAsync(cancellationToken);
    }
}