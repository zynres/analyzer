using Analyzer.Html.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Analyzer.Html.Services;

namespace Analyzer.Html.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ElementsController : ControllerBase
{
    private readonly ElementProcessingService elementProcessing;

    public ElementsController(ElementProcessingService elementProcessing)
    {
        this.elementProcessing = elementProcessing;
    }

    [HttpPost]
    public async Task<ActionResult<PostElementResponse>> CreateElement(PostElementRequest request)
    {
        (int statusCode, PostElementResponse response) = await elementProcessing.ProcessAsync(request);

        return StatusCode(statusCode, response);
    }
}
