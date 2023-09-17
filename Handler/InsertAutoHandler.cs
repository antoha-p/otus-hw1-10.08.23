using Bogus;
using HomeWork.DB;
using HomeWork.Entities;
using HomeWork.Interface;

namespace HomeWork.Handler;

public class InsertAutoHandler : IHandler
{
    /// <summary>
    /// Кол-во генерированных данных
    /// </summary>
    private const int INSERT_COUNT = 5;

    /// <inheritdoc/>
    public void Run()
    {
        Console.WriteLine("Заполняем таблицы данными...\n");

        using var context = new DataContext();

        for (var i = 0; i < INSERT_COUNT; i++)
        {
            Generate(out var firstName, out var lastName, out var passportNumber, out var accountAmount);

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
                Console.WriteLine("Клиент добавлен:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Id={client.Id} | FirstName={client.FirstName} | LastName={client.LastName} | PassportNumber={client.Passports.First().Number} | AccountAmount={client.Accounts.First().Amount}");
                Console.ResetColor();
            }
            else
            {
                throw new Exception("Ошибка добавления клиента.");
            }
        }
    }

    /// <summary>
    /// Генерирует случайные данные.
    /// </summary>
    /// <param name="firstName">Имя.</param>
    /// <param name="lastName">Фамилия.</param>
    /// <param name="passportNumber">Номер паспорт.</param>
    /// <param name="accountAmount">Сумма на аккаунте.</param>
    private static void Generate(
        out string firstName,
        out string lastName,
        out string passportNumber,
        out decimal accountAmount)
    {
        var faker = new Faker();

        firstName = faker.Person.FirstName;
        lastName = faker.Person.LastName;
        passportNumber = faker.Random.Number(1000, 9999) + " " + faker.Random.Number(100000, 999999);
        accountAmount = Math.Round(faker.Random.Decimal(0, 999999), 2);
    }
}
