using AutoMapper;
using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Models.Paging;
using DigitalStudio.InvoiceManagement.WebApi.Models.Views;
using DigitalStudio.InvoiceManagement.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DescriptionAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute;

namespace DigitalStudio.InvoiceManagement.WebApi.Controllers;

[ApiController]
[Route("/api/invoices")]
[Produces("application/json")]
public class InvoiceController : ControllerBase
{
    private readonly IMapper _mapper;

    private readonly AppDataContext _appDataContext;

    public InvoiceController(IMapper mapper, AppDataContext appDataContext)
    {
        _mapper = mapper;
        _appDataContext = appDataContext;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var invoice = await _appDataContext
            .Invoices.FirstOrDefaultAsync(i => i.Id == id);

        return Ok(invoice);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetListAsync([FromQuery] PageInfo pageInfo)
    {
        var invoices = await _appDataContext
            .Invoices
            .Include(i=> i.PaymentWay)
            .Include(i => i.ProcessingStatus)
            .Skip(pageInfo.Size * (pageInfo.No - 1))
            .Take(pageInfo.Size)
            .ToListAsync();

        return Ok(invoices);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] InvoiceModel model)
    {
        var editInvoice = _mapper.Map<InvoiceDataModel>(model);

        var entityEntry = model.Id == null
            ? await _appDataContext.Invoices.AddAsync(editInvoice)
            : _appDataContext.Invoices.Update(editInvoice);

        await _appDataContext.SaveChangesAsync();

        return Ok(entityEntry.Entity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deletingInvoice = await _appDataContext.FindAsync<InvoiceDataModel>(id);

        if (deletingInvoice == null)
        {
            return BadRequest();
        }

        _appDataContext.Invoices.Remove(deletingInvoice);
        await _appDataContext.SaveChangesAsync();

        return Ok(deletingInvoice);
    }
}