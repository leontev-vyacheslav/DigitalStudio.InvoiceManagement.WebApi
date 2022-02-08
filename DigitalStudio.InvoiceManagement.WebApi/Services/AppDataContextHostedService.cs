namespace DigitalStudio.InvoiceManagement.WebApi.Services;

public class AppDataContextHostedService: IHostedService
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
        await appDataContext.Invoices.AddRangeAsync(await persistentStorageService.LoadAsync(cancellationToken), cancellationToken);

        await appDataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var appDataContext = scope.ServiceProvider.GetRequiredService<AppDataContext>();

        await appDataContext.SaveChangesAsync(cancellationToken);
    }
}