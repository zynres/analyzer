using Analyzer.Html.Models.Common;
using Analyzer.Html.Models.Dtos;
using System.Text;

namespace Analyzer.Html.Services;

public sealed class ElementParserService
{
    private readonly AesService aesService;

    public ElementParserService(AesService aesService)
    {
        this.aesService = aesService;
    }

    public int TryParse(PostElementRequest request, PostElementResponse response, out string page) // return status code if not 0
    {
        page = string.Empty;

        byte[] textBuffer;
        byte[] keyBuffer;

        try
        {
            textBuffer = Convert.FromBase64String(request.EncryptedTextBytesB64);
        }
        catch (FormatException ex)
        {
            return response.ToError(
                ErrorCodeType.INVALID_DECODE_ENCRYPTED_TEXT_B64,
                $"URL contains invalid - encrypted_text_bytes_base64: {ex.Message}",
                statusCode: 400);
        }

        try
        {
            keyBuffer = Convert.FromBase64String(request.KeyBytesB64);
        }
        catch (FormatException ex)
        {
            return response.ToError(
                ErrorCodeType.INVALID_DECODE_KEY_BYTES_B64,
                $"URL contains invalid - key_bytes_base64: {ex.Message}",
                statusCode: 400);
        }

        // decrypting process:

        int result = aesService.TryDecrypt(textBuffer, keyBuffer, response, out string value);

        if (result != 0)
            return result;

        response.DecryptedPlainText = value;

        try
        {
            ReadOnlySpan<byte> urlBytes = Convert.FromBase64String(request.UrlB64);

            response.Url = Encoding.UTF8.GetString(urlBytes);
        }
        catch (FormatException ex)
        {
            return response.ToError(
                ErrorCodeType.INVALID_DECODE_URL_B64,
                $"URL contains invalid - url_base64: {ex.Message}",
                statusCode: 400);
        }

        try
        {
            ReadOnlySpan<byte> pageBytes = Convert.FromBase64String(request.PageB64);

            page = Encoding.UTF8.GetString(pageBytes);
        }
        catch (FormatException ex)
        {
            return response.ToError(
                ErrorCodeType.INVALID_DECODE_PAGE_B64,
                $"URL contains invalid - page_base64: {ex.Message}",
                statusCode: 400);
        }

        return 0;
    }
}
