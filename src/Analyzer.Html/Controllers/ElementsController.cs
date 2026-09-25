using Analyzer.Html.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Analyzer.Html.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ElementsController : ControllerBase
{
    public IValidator<PostElementRequest> validator;

    public ElementsController(IValidator<PostElementRequest> validator)
    {
        this.validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateElement(PostElementRequest request)
    {
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {

        }

        return Created();
    }
}
