using System.Globalization;
using System.Text;
using DigitalStudio.InvoiceManagement.WebApi.Models;

namespace DigitalStudio.InvoiceManagement.WebApi.Services;

public class PersistentStorageService
{
    public async Task<IEnumerable<InvoiceDataModel>> LoadAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var records = await File.ReadAllLinesAsync(@"c:\Temp\Invoices.csv", cancellationToken);

        var invoices = records.Select(row =>
        {
            var rowData = row.Split(";");

            return new InvoiceDataModel
            {
                Id = Convert.ToInt32(rowData[0]),
                CreationDate = DateTime.Parse(rowData[1], CultureInfo.InvariantCulture),
                ChangeDate = DateTime.Parse(rowData[2], CultureInfo.InvariantCulture),
                ProcessingStatusId = Convert.ToInt32(rowData[3]),
                PaymentWayId = Convert.ToInt32(rowData[4]),
                Amount = Convert.ToDecimal(rowData[5])
            };
        });

        return invoices;
    }

    public async Task SaveAsync(IEnumerable<InvoiceDataModel> invoices, CancellationToken cancellationToken = new CancellationToken())
    {
        var csv = new StringBuilder();

        invoices.ToList().ForEach(invoice =>
        {
            csv.AppendLine(string.Join(";", new []
            {
                invoice.Id.ToString(),
                invoice.CreationDate.ToString(CultureInfo.InvariantCulture),
                invoice.ChangeDate.ToString(CultureInfo.InvariantCulture),
                invoice.ProcessingStatusId.ToString(),
                invoice.PaymentWayId.ToString(),
                invoice.Amount.ToString(CultureInfo.InvariantCulture)
            }));
        });

        await File.WriteAllTextAsync(@"c:\Temp\Invoices.csv", csv.ToString(), cancellationToken);
    }
}