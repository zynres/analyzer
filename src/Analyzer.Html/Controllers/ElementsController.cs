using Analyzer.Html.Models.Common;
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
        
        var response = new PostElementResponse();

        if (!result.IsValid)
        {
            var error = result.Errors[0];

            response.IsError = 1;
            response.ErrorCode = error.ErrorCode;
            response.ErrorMessage = error.ErrorMessage;

            return BadRequest(response);
        }

        response.ErrorCode = ErrorCodeType.NONE.ToString();

        return Created();
    }
}
