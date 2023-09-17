using HomeWork.Interface;
using Npgsql;

namespace HomeWork.Handler;

public class GetVersionHandler : IHandler
{
    /// <inheritdoc/>
    public void Run()
    {
        Console.WriteLine("GetVersionHandler...");

        var connectionString = "Host=localhost;Port=54321;Username=postgres_user;Password=postgres_password;Database=postgres_db";

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var sql = "SELECT version()";

        using var cmd = new NpgsqlCommand(sql, connection);

        string? version = cmd.ExecuteScalar()?.ToString();

        Console.WriteLine($"PostgreSQL version: {version}");
    }
}
