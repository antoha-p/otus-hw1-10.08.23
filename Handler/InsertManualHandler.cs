using HomeWork.DB;
using HomeWork.Entities;
using HomeWork.Interface;

namespace HomeWork.Handler;

public class InsertManualHandler : IHandler
{
    /// <inheritdoc/>
    public void Run()
    {
        Console.WriteLine("Добавляем нового клиента в таблицу:\n");

        var firstName = ReadField("Введите Имя: ");
        var lastName = ReadField("Введите Фамилия: ");
        var passportNumber = ReadField("Введите Номер паспорта (xxxx xxxxxx): ");
        var accountAmount = decimal.Parse(ReadField("Введите Сумма аккаунта: "));
        // TODO: add validation

        Console.WriteLine();

        using var context = new DataContext();

        var client = new Client
        {
            FirstName = firstName,
            LastName = lastName,
            Passports = new List<Passport>
            {
                new()
                {
                    Number = passportNumber
                }
            },
            Accounts = new List<Account>
            {
                new()
                {
                    Amount = accountAmount,
                }
            }
        };

        context.Clients.Add(client);

        if (context.SaveChanges() > 0)
        {
            Console.WriteLine("Данные клиента успешно сохранены.");
        }
        else
        {
            throw new Exception("Ошибка добавления клиента.");
        }
    }

    private static string ReadField(string message)
    {
        Console.Write(message);

        Console.ForegroundColor = ConsoleColor.Red;
        var value = Console.ReadLine();
        Console.ResetColor();

        if (value is null)
        {
            throw new IOException();
        }

        return value;
    }
}
