using HomeWork.DB;
using HomeWork.Interface;

namespace HomeWork.Handler;

public class ShowDataHandler : IHandler
{
    /// <inheritdoc/>
    public void Run()
    {
        Console.WriteLine("Выводим данные таблиц...\n");

        using var context = new DataContext();

        Console.WriteLine("Таблица Clients:");
        WriteEntities(context.Clients.ToList());

        Console.WriteLine("Таблица Passports:");
        WriteEntities(context.Passports.ToList());

        Console.WriteLine("Таблица Accounts:");
        WriteEntities(context.Accounts.ToList());
    }

    /// <summary>
    /// Выводит сущности в консоль.
    /// </summary>
    /// <param name="entities">Сущности.</param>
    private static void WriteEntities(IEnumerable<IEntity> entities)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        foreach (var entity in entities)
        {
            Console.WriteLine(entity);
        }

        Console.WriteLine();
        Console.ResetColor();
    }
}
