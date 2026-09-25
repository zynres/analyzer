namespace Analyzer.Html.Models.Entities;

public sealed class Element
{
    public int Id { get; set; }

    public string AttributeValue { get; set; } = string.Empty;

    public string Html { get; set; } = string.Empty;
}
