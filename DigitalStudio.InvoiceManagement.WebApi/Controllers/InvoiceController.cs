using AutoMapper;
using DigitalStudio.InvoiceManagement.WebApi.Models;
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

    [HttpGet("list")]
    public async Task<IActionResult> GetListAsync()
    {
        var invoices = await _appDataContext.Invoices.ToListAsync();

        return Ok(invoices);
    }

    [HttpPut("create")]
    public async Task<IActionResult> PutAsync(InvoiceModel model)
    {
        var invoiceEntity = await _appDataContext.Invoices.AddAsync(_mapper.Map<InvoiceDataModel>(model));
        await _appDataContext.SaveChangesAsync();

        return Ok(invoiceEntity.Entity);
    }
}