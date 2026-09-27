using Analyzer.Html.Services.Repositories;
using Analyzer.Html.Models.Common;
using Analyzer.Html.Models.Dtos;
using FluentValidation;

namespace Analyzer.Html.Services;

public sealed class ElementProcessingService
{
    private readonly IValidator<PostElementRequest> validator;

    private readonly EmailExtractorService emailExtractor;
    private readonly ElementParserService elementParser;
    private readonly ElementRepository repository;
    private readonly HtmlParserService htmlParser;

    public ElementProcessingService(IValidator<PostElementRequest> validator, EmailExtractorService emailExtractor, ElementRepository repository, ElementParserService elementParser, HtmlParserService htmlParser)
    {
        this.emailExtractor = emailExtractor;
        this.elementParser = elementParser;
        this.repository = repository;
        this.htmlParser = htmlParser;
        this.validator = validator;
    }

    public async Task<(int StatusCode, PostElementResponse Response)> ProcessAsync(PostElementRequest request)
    {
        var result = await validator.ValidateAsync(request);

        var response = new PostElementResponse()
        {
            ErrorCode = ErrorCodeType.NONE.ToString(),
            ErrorMessage = "."
        };

        if (!result.IsValid)
        {
            var error = result.Errors[0];

            response.ToError(error.ErrorCode, error.ErrorMessage);

            return (400, response);
        }

        int parserResult = elementParser.TryParse(request, response, out string page);

        if (parserResult != 0)
            return (parserResult, response);

        HtmlParseResult parsedHtml;

        try
        {
            parsedHtml = await htmlParser.ParseAsync(page, request.Selector, request.Attribute);

            response.ElementsCount = parsedHtml.ElementsAttributeList.Count;
            response.ElementsAttributeList = parsedHtml.ElementsAttributeList;
        }
        catch (ArgumentException ex)
        {
            response.ToError(
                ErrorCodeType.INVALID_PARSING_HTML_PAGE,
                $"HtmlParser failed to parse decoded_page: {ex.Message}");

            return (400, response);
        }

        if (!await repository.InsertManyAsync(parsedHtml.ElementsAttributeList, parsedHtml.ElementsHtmlList))
        {
            response.ToError(
                ErrorCodeType.INVALID_INSERTING_BY_TRANSACTION,
                "Invalid transaction.");

            return (500, response);
        }

        var emails = emailExtractor.Extract(page);

        response.EmailsCount = emails.Count;
        response.EmailsList = emails;

        return (200, response);
    }
}
