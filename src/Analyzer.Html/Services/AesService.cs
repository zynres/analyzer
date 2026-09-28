using System.Security.Cryptography;
using System.Text;
using Analyzer.Html.Models.Common;
using Analyzer.Html.Models.Dtos;

namespace Analyzer.Html.Services;

public sealed class AesService
{
    public int TryDecrypt(byte[] encryptedBytes, byte[] keyBytes, PostElementResponse response, out string value) // return status code if not 0
    {
        value = string.Empty;

        if (keyBytes.Length != 32)
            return response.ToError(
                ErrorCodeType.AES256_KEY_NOT_32B,
                "AES-256 key must contain 32 bytes.",
                statusCode: 400);

        if (encryptedBytes.Length % 16 != 0)
            return response.ToError(
                ErrorCodeType.AESECB_DATA_LENGTH_NOT_MULTIPLY_OF_16B,
                "AES ECB data length must be a multiple of 16 bytes.",
                statusCode: 400);

        try
        {
            using Aes aes = Aes.Create();

            aes.Key = keyBytes;
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.None;

            using ICryptoTransform decryptor = aes.CreateDecryptor();

            ReadOnlySpan<byte> plaintext = decryptor.TransformFinalBlock(
                encryptedBytes,
                0,
                encryptedBytes.Length);

            value = Encoding.UTF8.GetString(plaintext);
            return 0;
        }
        catch (CryptographicException ex)
        {
            return response.ToError(
                ErrorCodeType.INVALID_AES_DATA,
                ex.Message,
                400);
        }
    }
}
