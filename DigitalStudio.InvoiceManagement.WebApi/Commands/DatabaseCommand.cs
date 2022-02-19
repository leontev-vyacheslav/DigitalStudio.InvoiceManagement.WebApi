using DigitalStudio.InvoiceManagement.WebApi.Services;

namespace DigitalStudio.InvoiceManagement.WebApi.Commands;

public abstract class DatabaseCommand
{
    protected readonly AppDataContext AppDataContext;

    protected DatabaseCommand(AppDataContext appDataContext)
    {
        AppDataContext = appDataContext;
    }
}