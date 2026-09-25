using Microsoft.AspNetCore.Mvc;

namespace Analyzer.Html.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ElementsController : ControllerBase 
{
    [HttpPost]
    public async Task<IActionResult> CreateElement() 
    {
        return Created();
    }
}
