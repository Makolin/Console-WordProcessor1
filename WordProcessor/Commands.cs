using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using System.Linq;
using System.IO;

namespace WordProcessor
{
    // Описание команд, которые могут быть выполнены словарем 
    class Commands
    {
        // Функция для преобразования текста в словарь
        static string[] SplitText (string inputText)
        {
            char[] SeparationCharacters = new char[] { '.', ';', ',', '!', '?', '(', '(', '\n', '\r', ' ' };

            string[] arrayWords = inputText.ToLower().Split(SeparationCharacters);

            return arrayWords;
        }

        // Команда создания словаря - формирование нового словаря по входящему файлу
        public static void CreateDictionary(string path)
        {
            var inputText = string.Empty;
            inputText = File.ReadAllText(path, Encoding.UTF8);

            using (var db = new WordContext())
            {
                // Вывод сообщения о неправильно выбранной команде
                if (db.Words.Count() != 0)
                {
                    Console.WriteLine("Словарь уже заполнен, его необходимо очистить, либо выбрать команду обновить.");
                    return;
                }

                // Заполнение базы данных словами, после разбития текста на отдельные слова
                var arrayWords = SplitText(inputText);
                foreach (var oneWord in arrayWords)
                {
                    var word = new Word { WordName = oneWord };
                    db.Words.Add(word);
                }
                db.SaveChanges();
            }
            Console.WriteLine("Словарь успешно заполнен!");
        }
        // Команда обновления словаря - дополнение существующего словаря по входящему файлу
        public static void UpdateDictionary()
        {
            using (var db = new WordContext())
            {
                var word = new Word { WordName = "Привет" };
                db.Words.Add(word);
                db.SaveChanges();
            }
            Console.WriteLine("Словарь успешно дополнен!");
        }
        // Команда очищения словаря - удаление всех данных из словаря
        public static void ClearDictionary()
        {
            using (var db = new WordContext())
            {
                db.Words.RemoveRange(db.Words);
                db.SaveChanges();
            }
            Console.WriteLine("Словарь очищен");
        }
        // Команда для демонстрации количесва слов в словаре
        public static void ReportDistionary()
        {
            using (var db = new WordContext())
            {
                var query = from word in db.Words
                            select word;

                if (query.Count() == 0)
                    Console.WriteLine("В словаре нет ни одного значения");
                else
                    Console.WriteLine($"В текущий момент в словаре {query.Count()} слов(а).");
            }
        }
        // Подбор значений для введенной строки
        public static void AddToWords()
        {
            Console.Write("Введите первую часть слова: ");
            var prefix = Console.ReadLine();
            List <string> exitWords = new List <string>();
            List<string> Temp = new List<string>();

            while (true)
            {
                using (var db = new WordContext())
                {
                    var query = from word in db.Words
                                where word.WordName.Contains(prefix.ToLower().ToString())
                                select word;
                    if (query.Count() != 0)
                    {
                        foreach (var addWord in query)
                        {
                            Console.WriteLine("- " + addWord.WordName);
                        }
                    }
                    else
                        Console.WriteLine("Словарь пуст, необходимо внести значения!");
                }
            }
        }
    }
}
