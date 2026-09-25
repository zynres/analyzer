using Analyzer.Html.Models.Entities;
using Dapper;
using Npgsql;

namespace Analyzer.Html.Services.Repositories;

public sealed class ElementRepository
{
    private readonly string connectionString;

    public ElementRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("Db")!;
    }

    public async Task InsertAsync(Element element)
    {
        await using var connection =
            new NpgsqlConnection(connectionString);

        await connection.OpenAsync();

        await connection.ExecuteAsync(
            """
            INSERT INTO elements (attribute_value, html)
            VALUES (@AttributeValue, @Html)
            """,
            new
            {
                element.AttributeValue,
                element.Html
            });
    }
}
