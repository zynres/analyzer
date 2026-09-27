namespace Analyzer.Html.Models.Dtos;

public sealed class HtmlParseResult
{
    public List<string> ElementsAttributeList { get; set; } = [];
    public List<string> ElementsHtmlList { get; set; } = [];
}
