using Analyzer.Html.Models.Common;
using Analyzer.Html.Models.Dtos;
using FluentValidation;

namespace Analyzer.Html.Services.Validators;

public class PostElementRequestValidator
    : AbstractValidator<PostElementRequest>
{
    public PostElementRequestValidator()
    {
        RuleFor(x => x)
            .Must(IsParametrsNotEmpty)
            .WithErrorCode(ErrorCodeType.MISSING_ALL_PARAMETRS.ToString())
            .WithMessage("Parametrs must be filled.");

        When(IsParametrsNotEmpty, () =>
        {
            RuleFor(x => x.Selector)
            .NotEmpty()
            .WithErrorCode(ErrorCodeType.EMPTY_SELECTOR.ToString())
            .WithMessage("selector cannot be empty.");

            RuleFor(x => x.Attribute)
            .NotEmpty()
            .WithErrorCode(ErrorCodeType.EMPTY_ATTRIBUTE.ToString())
            .WithMessage("attribute cannot be empty.");

            RuleFor(x => x.UrlB64)
            .NotEmpty()
            .WithErrorCode(ErrorCodeType.EMPTY_URL_B64.ToString())
            .WithMessage("url_b64 cannot be empty.");

            RuleFor(x => x.EncryptedTextBytesB64)
            .NotEmpty()
            .WithErrorCode(ErrorCodeType.EMPTY_ENCRYPTED_TEXT_B64.ToString())
            .WithMessage("encrypted_text_bytes_b64 cannot be empty.");

            RuleFor(x => x.KeyBytesB64)
            .NotEmpty()
            .WithErrorCode(ErrorCodeType.EMPTY_KEY_BYTES_B64.ToString())
            .WithMessage("key_bytes_b64 cannot be empty");

            RuleFor(x => x.PageB64)
            .NotEmpty()
            .WithErrorCode(ErrorCodeType.EMPTY_PAGE_B64.ToString())
            .WithMessage("page_b64 cannot be empty");
        });
    }

    private bool IsParametrsNotEmpty(PostElementRequest x)
    {
        return !string.IsNullOrWhiteSpace(x.Selector)
            || !string.IsNullOrWhiteSpace(x.Attribute)
            || !string.IsNullOrWhiteSpace(x.UrlB64)
            || !string.IsNullOrWhiteSpace(x.EncryptedTextBytesB64)
            || !string.IsNullOrWhiteSpace(x.KeyBytesB64)
            || !string.IsNullOrWhiteSpace(x.PageB64);
    }
}
