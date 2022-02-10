using DigitalStudio.InvoiceManagement.Domain.Contracts;
using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalStudio.InvoiceManagement.WebApi.Controllers;

[ApiController]
[Route("/api/dictionaries")]
[Produces("application/json")]
public class DictionaryController : ControllerBase
{
    private readonly AppDataContext _appDataContext;

    public DictionaryController(AppDataContext appDataContext)
    {
        _appDataContext = appDataContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(string name)
    {
        var dictionary = $"{name}DataModel" switch
        {
            nameof(PaymentWayDataModel) => await _appDataContext.PaymentWays.ToListAsync(),
            nameof(ProcessingStatusDataModel) => await _appDataContext.ProcessingStatuses.ToListAsync(),
            _ => null as IEnumerable<DictionaryBaseModel>
        };

        return Ok(dictionary);
    }
}