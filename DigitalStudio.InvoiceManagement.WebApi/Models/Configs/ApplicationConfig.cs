namespace DigitalStudio.InvoiceManagement.WebApi.Models.Configs;

public sealed class ApplicationConfig: IApplicationConfig
{
    public string CommonNamespace { get; set; } = string.Empty;

    public string ServiceName { get; set; } = string.Empty;
}