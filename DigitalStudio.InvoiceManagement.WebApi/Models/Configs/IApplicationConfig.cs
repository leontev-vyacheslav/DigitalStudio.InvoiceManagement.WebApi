namespace DigitalStudio.InvoiceManagement.WebApi.Models.Configs;

public interface IApplicationConfig
{
    string CommonNamespace { get; set; }

    string ServiceName { get; set; }
}