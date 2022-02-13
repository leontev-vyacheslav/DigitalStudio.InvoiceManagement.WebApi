using DigitalStudio.InvoiceManagement.WebApi.Commands.Dictionary;
using DigitalStudio.InvoiceManagement.WebApi.Immutables;
using Microsoft.AspNetCore.Mvc;
using DescriptionAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute;

namespace DigitalStudio.InvoiceManagement.WebApi.Controllers;

[ApiController]
[Route("/api/dictionaries")]
[Produces("application/json")]
public class DictionaryController : ControllerBase
{

    [Description(AttributeStrings.GetDictionary)]
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromServices] GetDictionaryCommand command, string name)
    {
        var dictionary = await command.GetAsync(name);

        return Ok(dictionary);
    }
}