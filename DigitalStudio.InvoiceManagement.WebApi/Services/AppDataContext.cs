using DigitalStudio.InvoiceManagement.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalStudio.InvoiceManagement.WebApi.Services;

public class AppDataContext : DbContext
{
    private readonly PersistentStorageService _persistentStorageService;

    public AppDataContext(DbContextOptions<AppDataContext> options, PersistentStorageService persistentStorageService) : base(options)
    {
        _persistentStorageService = persistentStorageService;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var count = base.SaveChanges(acceptAllChangesOnSuccess);
        _persistentStorageService.SaveAsync(Invoices).GetAwaiter().GetResult();

        return count;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var count = await base.SaveChangesAsync(cancellationToken);
        await _persistentStorageService.SaveAsync(Invoices, cancellationToken);

        return count;
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        var count = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);;
        await _persistentStorageService.SaveAsync(Invoices, cancellationToken);

        return count;
    }

    public DbSet<InvoiceDataModel> Invoices { get; set; }
}