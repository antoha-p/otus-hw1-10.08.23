using HomeWork.Interface;
using Npgsql;
using System.Configuration;

namespace HomeWork.Handler;

public class CreateTablesHandler : IHandler
{
    private readonly string _connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

    /// <inheritdoc/>
    public void Run()
    {
        ShowInfo();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var cmd = new NpgsqlCommand(GetSql(), connection);

        cmd.ExecuteNonQuery();

        Console.WriteLine("Таблицы созданы успешно.");

    }

    private static void ShowInfo()
    {
        Console.WriteLine("Создаём таблицы...\n");
        Console.WriteLine("Выполняем скрипт (ADO.NET):");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(GetSql());
        Console.ResetColor();
    }

    private static string GetSql()
    {
        return @"
CREATE TABLE IF NOT EXISTS ""Clients"" (
    ""Id"" serial NOT NULL PRIMARY KEY,
    ""FirstName"" varchar(255) NOT NULL,
    ""LastName"" varchar(255) NOT NULL,
    ""CreatedAt"" TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW(),
    ""UpdatedAt"" TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS ""Passports""
(
    ""Id"" serial NOT NULL,
    ""ClientId"" serial,
    ""Number"" varchar(11),
    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),
    ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT NOW(),

    CONSTRAINT ""PK_Passports"" PRIMARY KEY (""Id""),
    CONSTRAINT ""FK_Passports_Clients_ClientId"" FOREIGN KEY (""ClientId"") REFERENCES public.""Clients""(""Id"") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ""Accounts""
(
    ""Id"" serial NOT NULL,
    ""ClientId"" serial,
    ""Amount"" decimal(15,2),
    ""CreatedAt"" TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    ""UpdatedAt"" TIMESTAMPTZ NOT NULL DEFAULT NOW(),

    CONSTRAINT ""PK_Accounts"" PRIMARY KEY (""Id""),
    CONSTRAINT ""FK_Accounts_Clients_ClientId"" FOREIGN KEY (""ClientId"") REFERENCES public.""Clients""(""Id"") ON DELETE CASCADE
);
";
    }
}
