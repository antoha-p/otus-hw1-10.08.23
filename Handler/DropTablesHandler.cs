using HomeWork.Interface;
using Npgsql;

namespace HomeWork.Handler;

public class DropTablesHandler : IHandler
{
    private readonly string _connectionString = "Host=localhost;Port=54321;Username=postgres_user;Password=postgres_password;Database=postgres_db";

    /// <inheritdoc/>
    public void Run()
    {
        ShowInfo();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand(GetSql(), connection);

        cmd.ExecuteNonQuery();

        Console.WriteLine("Таблицы удалены успешно.");

    }

    private static void ShowInfo()
    {
        Console.WriteLine("Удаляем таблицы...\n");
        Console.WriteLine("Выполняем скрипт (ADO.NET):");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(GetSql());
        Console.ResetColor();
    }

    private static string GetSql()
    {
        return @"
DROP TABLE IF EXISTS ""Clients"", ""Passports"", ""Accounts"" CASCADE;
";
    }
}
