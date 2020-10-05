using System;

namespace WordProcessor
{
    class Program
    {
        // Вывод информации об основных параметрах, используемых для данного приложения
        public static void ShowCommand()
        {
            Console.WriteLine("Выберете один из представленных параметров:");
            Console.WriteLine("1. Создание словаря из файла (-c)");
            Console.WriteLine("2. Обновление словаря из файла. (-u)");
            Console.WriteLine("3. Очистка словаря из файла. (-d)");
            Console.WriteLine("4. Отобразить количество слов в словаре. (-i)");
            Console.WriteLine("5. Ввод значений, подбор слов. (Запуск без аргументов)");
            Console.WriteLine("6. Выйти из приложения. (-q)");
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
                    case "-q":
                        return;
                    default:
                        Console.WriteLine("Введена неверная команда. Для справки необходимо ввести параметр -h");
                        return;
                }
            }
        }
    }
}
