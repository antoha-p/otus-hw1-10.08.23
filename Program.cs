using HomeWork.Handler;
using HomeWork.Interface;
using HomeWork.Menu;

namespace HomeWork;

public static class Program
{
    /// <summary>
    /// Входная точка приложения.
    /// </summary>
    private static void Main()
    {
        ShowTopInfo();
        Menu(Console.CursorTop + 1);
    }

    /// <summary>
    /// Меню
    /// </summary>
    /// <param name="cursorTopPosition"></param>
    private static void Menu(int cursorTopPosition)
    {
        IMenu menu = new MenuHandler(GetMenuItems(), cursorTopPosition, 0);

        var cursorTop = 0;

        while (true)
        {
            var selection = menu.RunMenu();

            // очищаем информативную часть консоли
            ClearBottomInfo(cursorTop);

            try
            {
                menu.GetMenuItem(selection).Handler.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            // запоминаем текущее положение курсора для дальнейшей очистки консоли
            cursorTop = Console.CursorTop;
        }
    }

    /// <summary>
    /// Возвращает пункты меню
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<IMenuItem> GetMenuItems()
    {
        var item1 = new MenuItem
        (
            "Создать 3 таблицы (1)",
            new CreateTablesHandler()
        );

        var item2 = new MenuItem
        (
            "Заполнить таблицу данными (2)",
            new InsertAutoHandler()
        );

        var item3 = new MenuItem
        (
            "Вывести содержимое таблиц (3)",
            new ShowDataHandler()
        );

        var item4 = new MenuItem
        (
            "Добавить сущность в таблицу (4)",
            new InsertManualHandler()
        );

        var item5 = new MenuItem
        (
            "Удалить все таблицы",
            new DropTablesHandler()
        );

        var item6 = new MenuItem
        (
            "Выход",
            new ExitHandler()
        );

        IMenuItem[] menuItems = { item1, item2, item3, item4, item5, item6 };

        return menuItems;
    }

    /// <summary>
    /// Очищает нижнюю область с выводом информации
    /// </summary>
    private static void ClearBottomInfo(int cursorTop = 0)
    {
        var currentLineCursor = Console.CursorTop;

        for (var i = currentLineCursor; i < cursorTop + 1; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.WindowWidth));
        }

        Console.SetCursorPosition(0, 0);
        Console.SetCursorPosition(0, currentLineCursor);
    }

    /// <summary>
    /// Выводит основную информацию
    /// </summary>
    private static void ShowTopInfo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Используйте кнопки вверх и вниз для навигации по меню и Enter для выбора.\n");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("1. Написать скрипт создания 3 таблиц, которые должны иметь первичные ключи и быть соединены внешними ключами.");
        Console.WriteLine("2. Написать скрипт заполнения таблиц данными, минимум по пять строк в каждую.");
        Console.WriteLine("3. Создать консольную программу, которая выводит содержимое всех таблиц.");
        Console.WriteLine("4. Добавить в программу возможность добавления в таблицу на выбор.");
    }
}
