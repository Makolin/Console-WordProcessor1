using System;

namespace WordProcessor
{
    class Program
    {
        // Вывод информации об основных параметрах, используемых для данного приложения
        public static void ShowCommand()
        {
            Console.WriteLine("Текстовый редактор поддерживает следующие параметры:");
            Console.WriteLine("1. Создание словаря из файла (-c)");
            Console.WriteLine("2. Обновление словаря из файла. (-u)");
            Console.WriteLine("3. Очистка словаря. (-d)");
            Console.WriteLine("4. Отображение количества слов в словаре. (-i)");
            Console.WriteLine("5. Основная функция редактора - подбор слов. (Запуск без аргументов)");
        }

        // Ограничение в использовании всего одного параметра
        static void Main(string[] args)
        {
            while (true)
            {
                if (args.Length == 0)
                {
                    MainCommands.AddToWords();
                    break;
                }

                switch (args[0])
                {
                    case "-c":
                        MainCommands.CreateDictionary();
                        return;
                    case "-u":
                        MainCommands.UpdateDictionary();
                        return;
                    case "-d":
                        MainCommands.ClearDictionary();
                        return;
                    case "-i":
                        MainCommands.ReportDistionary();
                        return;
                    case "-h":
                        ShowCommand();
                        return;
                    default:
                        Console.WriteLine("Редактор не поддерживает данный параметр, для ознакомления необходимо ввести -h");
                        return;
                }
            }
        }
    }
}
