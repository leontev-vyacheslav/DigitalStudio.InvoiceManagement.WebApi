using DigitalStudio.InvoiceManagement.WebApi.Immutables;
using Microsoft.AspNetCore.Mvc;
using DescriptionAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute;

namespace DigitalStudio.InvoiceManagement.WebApi.Controllers;

[ApiController]
[Route("/")]
public class DefaultController : ControllerBase
{
    [Description(AttributeStrings.Root)]
    [HttpGet]
    public IActionResult Root()
    {
        return Redirect("/docs/api");
    }
}