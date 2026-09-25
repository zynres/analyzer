using Analyzer.Html.Models.Dtos;
using FluentValidation;

namespace Analyzer.Html.Services.Validators;

public class PostElementRequestValidator 
    : AbstractValidator<PostElementRequest>
{
    public PostElementRequestValidator()
    {
        RuleFor(x => x.Selector)
            .NotEmpty();

        RuleFor(x => x.Attribute)
            .NotEmpty();

        RuleFor(x => x.UrlB64)
            .NotEmpty();

        RuleFor(x => x.EncryptedTextBytesB64)
            .NotEmpty();

        RuleFor(x => x.KeyBytesB64)
            .NotEmpty();

        RuleFor(x => x.PageB64)
            .NotEmpty();
    }
}
