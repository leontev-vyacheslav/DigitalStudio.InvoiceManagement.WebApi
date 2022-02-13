using DigitalStudio.InvoiceManagement.WebApi.Commands.Invoice;
using DigitalStudio.InvoiceManagement.WebApi.Immutables;
using DigitalStudio.InvoiceManagement.WebApi.Models.Paging;
using DigitalStudio.InvoiceManagement.WebApi.Models.Views;
using Microsoft.AspNetCore.Mvc;
using DescriptionAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute;

namespace DigitalStudio.InvoiceManagement.WebApi.Controllers;

[ApiController]
[Route("/api/invoices")]
[Produces("application/json")]
public class InvoiceController : ControllerBase
{
    [Description(AttributeStrings.GetInvoice)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync([FromServices] GetInvoiceCommand command, Guid id)
    {
        var invoice = await command.GetAsync(id);

        return Ok(invoice);
    }

    [Description(AttributeStrings.GetInvoiceList)]
    [HttpGet("list")]
    public async Task<IActionResult> GetListAsync([FromServices] GetInvoiceListCommand command, [FromQuery] PageInfo pageInfo)
    {
        var invoices = await command.GetAsync(pageInfo);

        return Ok(invoices);
    }

    [Description(AttributeStrings.PostInvoice)]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromServices] PostInvoiceCommand command, [FromBody] InvoiceModel model)
    {
        var invoice = await command.PostAsync(model);

        return Ok(invoice);
    }

    [Description(AttributeStrings.DeleteInvoice)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromServices] DeleteInvoiceCommand command, Guid id)
    {
        var deletingInvoice = await command.DeleteAsync(id);

        return deletingInvoice != null ? Ok(deletingInvoice) :  BadRequest();
    }
}