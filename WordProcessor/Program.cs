using System;

namespace WordProcessor
{
    class Program
    {
        // Знакомство с приложением
        public static void StartMessage()
        {
            Console.WriteLine("Выберете одну из трех команд:");
            Console.WriteLine("1. Создание словаря из файла");
            Console.WriteLine("2. Обновление словаря из файла.");
            Console.WriteLine("3. Очистка словаря из файла.");
            Console.WriteLine("4. Отобразить количество слов в словаре.");
            Console.WriteLine("5. Ввод значений, подбор слов.");
            Console.WriteLine("6. Выйти из приложения.");
        }

        static void Main(string[] args)
        {
            var path = @"C:\Users\Makolin\Desktop\Download.txt";

            StartMessage();
            // Выполнять команды, пока не будет произведен выход
            while (true)
            {
                Console.Write("Команда: ");
                var command = Console.ReadLine();

                // В зависимости от введеной команды пользотелем, выполняем различные сценарии
                switch (command)
                {
                    case "1":
                        Commands.CreateDictionary(path);
                        break;
                    case "2":
                        Commands.UpdateDictionary();
                        break;
                    case "3":
                        Commands.ClearDictionary();
                        break;
                    case "4":
                        Commands.ReportDistionary();
                        break;
                    case "5":
                        Commands.AddToWords();
                        return;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Введена неверная команда");
                        break;
                }
            }
        }
    }
}
