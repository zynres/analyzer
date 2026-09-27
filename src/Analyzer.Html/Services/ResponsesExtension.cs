using Analyzer.Html.Models.Common;
using Analyzer.Html.Models.Dtos;

namespace Analyzer.Html.Services;

public static class ResponsesExtension
{
    public static int ToError(this PostElementResponse response, ErrorCodeType errorType, string errorMessage, int statusCode)
    {
        response.IsError = 1;
        response.ErrorCode = errorType.ToString();
        response.ErrorMessage = errorMessage;

        return statusCode;
    }

    public static void ToError(this PostElementResponse response, ErrorCodeType errorType, string errorMessage)
    {
        response.IsError = 1;
        response.ErrorCode = errorType.ToString();
        response.ErrorMessage = errorMessage;
    }

    public static void ToError(this PostElementResponse response, string errorType, string errorMessage)
    {
        response.IsError = 1;
        response.ErrorCode = errorType;
        response.ErrorMessage = errorMessage;
    }
}
