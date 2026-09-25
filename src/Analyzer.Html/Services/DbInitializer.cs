using Dapper;
using Npgsql;

namespace Analyzer.Html.Services;

public static class DbInitializer
{
    public static void Initialize(IConfiguration configuration)
    {
        using var connection = new NpgsqlConnection(configuration.GetConnectionString("Db"));

        connection.OpenAsync();

        CreateElementsTable(connection);
    }

    private static void CreateElementsTable(NpgsqlConnection connection)
    {
        connection.Execute(
            """ 
                CREATE TABLE IF NOT EXISTS elements 
                (
                    id SERIAL PRIMARY KEY,
                    attribute_value TEXT NOT NULL,
                    html TEXT NOT NULL
                )       
            """);
    }
}
