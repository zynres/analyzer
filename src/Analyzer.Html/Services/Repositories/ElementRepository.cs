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

    public async Task<bool> InsertManyAsync(List<string> elementsAttribute, List<string> htmls)
    {
        await using var connection =
            new NpgsqlConnection(connectionString);

        await connection.OpenAsync();

        var transaction = await connection.BeginTransactionAsync();

        try
        {
            for (int i = 0; i < elementsAttribute.Count; i++)
            {
                await connection.ExecuteAsync(
                        """
                    INSERT INTO elements (attribute_value, html)
                    VALUES (@AttributeValue, @Html)
                    """,
                        new
                        {
                            AttributeValue = elementsAttribute[i],
                            Html = htmls[i]
                        },
                        transaction);
            }

            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();

            return false;
        }
    }
}
