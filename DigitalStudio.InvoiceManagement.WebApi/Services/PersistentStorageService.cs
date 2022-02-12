using System.Globalization;
using System.Text;
using DigitalStudio.InvoiceManagement.Domain.Models;

namespace DigitalStudio.InvoiceManagement.WebApi.Services;

public class PersistentStorageService
{
    private readonly string _storageRoot;

    public PersistentStorageService(IWebHostEnvironment webHostEnvironment)
    {
        _storageRoot = $"{webHostEnvironment.WebRootPath}/storage";
    }

    public async Task<IEnumerable<IEnumerable<object>>> LoadAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var records = await File.ReadAllLinesAsync($"{_storageRoot}/Invoices.csv", cancellationToken);

        var invoices = records.Select(row =>
        {
            var rowData = row.Split(";");

            return new InvoiceDataModel
            {
                Id = Guid.Parse(rowData[0]),
                CreationDate = DateTime.Parse(rowData[1], CultureInfo.InvariantCulture),
                ChangeDate = DateTime.Parse(rowData[2], CultureInfo.InvariantCulture),
                ProcessingStatusId = int.Parse(rowData[3]),
                PaymentWayId = int.Parse(rowData[4]),
                Amount = decimal.Parse(rowData[5])
            };
        });

        records = await File.ReadAllLinesAsync($"{_storageRoot}/PaymentWays.csv", cancellationToken);

        var paymentWays = records.Select(row =>
        {
            var rowData = row.Split(";");

            return new PaymentWayDataModel
            {
                Id = int.Parse(rowData[0]),
                Name = rowData[1]
            };
        });

        records = await File.ReadAllLinesAsync($"{_storageRoot}/ProcessingStatuses.csv", cancellationToken);

        var processingStatuses = records.Select(row =>
        {
            var rowData = row.Split(";");

            return new ProcessingStatusDataModel
            {
                Id = int.Parse(rowData[0]),
                Name = rowData[1]
            };
        });

        return new IEnumerable<object>[]{ invoices, paymentWays, processingStatuses};
    }

    public async Task SaveAsync(IEnumerable<InvoiceDataModel> invoices, CancellationToken cancellationToken = new CancellationToken())
    {
        var csv = new StringBuilder();

        invoices.ToList().ForEach(invoice =>
        {
            csv.AppendLine(string.Join(";", new []
            {
                invoice.Id.ToString("N"),
                invoice.CreationDate.ToString(CultureInfo.InvariantCulture),
                invoice.ChangeDate.ToString(CultureInfo.InvariantCulture),
                invoice.ProcessingStatusId.ToString(),
                invoice.PaymentWayId.ToString(),
                invoice.Amount.ToString(CultureInfo.InvariantCulture)
            }));
        });

        await File.WriteAllTextAsync($"{_storageRoot}/Invoices.csv", csv.ToString(), cancellationToken);
    }
}