using Microsoft.AspNetCore.Mvc;

namespace HtmlAnalyzer.Controllers;

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
