using System.Text.RegularExpressions;

namespace Analyzer.Html.Services;

public partial class EmailExtractorService
{
    public List<string> Extract(string page)
    {
        return EmailRegex()
            .Matches(page)
            .Select(match => match.Value)
            .ToList();
    }

    [GeneratedRegex(
        @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}")]
    private static partial Regex EmailRegex();
}
