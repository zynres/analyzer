using Analyzer.Html.Models.Dtos;
using AngleSharp.Dom;
using AngleSharp;

namespace Analyzer.Html.Services;

public sealed class HtmlParserService
{
    public async Task<HtmlParseResult> ParseAsync(string page, string selector, string attribute)
    {
        using var context = BrowsingContext.New(Configuration.Default);

        var document = await context.OpenAsync(request => request.Content(page));

        IHtmlCollection<IElement> elements = document.QuerySelectorAll(selector);

        var result = new HtmlParseResult();

        for (int i = 0; i < elements.Count; i++)
        {
            IElement element = elements[i];

            result.ElementsAttributeList.Add(
                element.GetAttribute(attribute) ?? string.Empty);

            result.ElementsHtmlList.Add(
                element.OuterHtml);
        }

        return result;
    }
}
