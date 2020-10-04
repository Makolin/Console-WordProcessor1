using System;

namespace WordProcessor
{
    class Program
    {
        // Знакомство с приложением
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
            var path = @"C:\Users\Makolin\Desktop\Download.txt";

            // Выполнять команды, пока не будет произведен выход
            while (true)
            {
                // Подстановка слов, если запущено без параметров
                if (args.Length == 0)
                {
                    MainCommands.AddToWords();
                    break;
                }

                // В зависимости от введенной команды пользователем, выполняем различные сценарии
                switch (args[0])
                {
                    case "-c":
                        MainCommands.CreateDictionary(path);
                        return;
                    case "-u":
                        MainCommands.UpdateDictionary(path);
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
