namespace DigitalStudio.InvoiceManagement.WebApi.Models.Configs;

public sealed class ApplicationConfig: IApplicationConfig
{
    public string CommonNamespace { get; set; }

    public string ServiceName { get; set; }
}