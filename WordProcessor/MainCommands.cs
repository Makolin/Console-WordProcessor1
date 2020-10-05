using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using System.Linq;
using System.IO;

namespace WordProcessor
{
    // Класс, который содержит описание команд, которые могут быть выполнены словарем 
    class MainCommands
    {
        // Команда создания словаря - формирование нового словаря по входящему файлу
        // Если словарь уже заполнен (имеет значения), то выводится соответствующее сообщение для пользователя
        public static void CreateDictionary()
        {
            var inputText = string.Empty;
            Dictionary<string, int> WordsDictionary = new Dictionary<string, int>();

            inputText = AdditionalCommands.CheckFile();
            if (inputText.Length == 0)
                return;

            using (var db = new WordContext())
            {
                if (db.Words.Count() != 0)
                {
                    Console.WriteLine("Ошибка! Словарь уже заполнен, его необходимо очистить, либо выбрать команду обновить словарь.");
                    return;
                }

                AdditionalCommands.SplitText(ref WordsDictionary, inputText);

                foreach (var oneWord in WordsDictionary)
                {
                    var word = new Word { WordName = oneWord.Key, WordFrequency = oneWord.Value };
                    db.Words.Add(word);
                }
                db.SaveChanges();
            }
            Console.WriteLine("Словарь успешно заполнен!");
        }

        // Команда обновления словаря - дополнение существующего словаря по входящему файлу
        // Данная команда объединяет значения, которые уже хранятся в базе данных с новыми, которые будут получены из файла
        // Если словарь пуст, то будет выведено соответствующее сообщение для пользователя
        public static void UpdateDictionary()
        {
            var inputText = string.Empty;
            Dictionary<string, int> WordsDictionary = new Dictionary<string, int>();

            inputText = AdditionalCommands.CheckFile();
            if (inputText.Length == 0)
                return;

            using (var db = new WordContext())
            {
                AdditionalCommands.InsertWords(ref WordsDictionary, inputText);

                foreach (var oneWord in WordsDictionary)
                {
                    if (db.Words.Count() == 0)
                    {
                        Console.WriteLine("Ошибка! Словарь пуст, необходимо выбрать команду заполнения пустого словаря.");
                        return;
                    }

                    var query = from find in db.Words
                                where find.WordName == oneWord.Key
                                select find;

                    if (query.Count() == 0)
                    {
                        var word = new Word { WordName = oneWord.Key, WordFrequency = oneWord.Value };
                        db.Words.Add(word);
                    }
                    else
                    {
                        query.FirstOrDefault().WordFrequency = oneWord.Value;
                        db.SaveChanges();
                    }
                }
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

        // Команда для демонстрации количества слов в словаре
        public static void ReportDistionary()
        {
            using (var db = new WordContext())
            {
                var query = from word in db.Words
                            select word;

                if (query.Count() == 0)
                    Console.WriteLine("Ошибка! В словаре нет ни одного значения");
                else
                    Console.WriteLine($"В текущий момент в словаре {query.Count()} слов(а).");
            }
        }

        // Основной метод для данного текстового процессора
        // При вводе слова или части слова, должен найти в существующем словаре подходящие слова, которые соответствуют логике автозаполнения
        // Вывод должен осуществляться в порядке частоты использования и по алфавиту, количество выведенных слов не превышать пяти
        // Если словарь пуст, то поиск не имеет смысла и будет выведено соответствующее сообщение для пользователя
        public static void AddToWords()
        {
            var prefix = string.Empty;
            while (true)
            {
                if (prefix == "")
                    Console.Write('>') ;

                var pressKey = Console.ReadKey(true);
                Console.Write(pressKey.KeyChar);

                if (pressKey.Key == ConsoleKey.Enter && prefix.Length == 0 || pressKey.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (pressKey.Key == ConsoleKey.Enter && prefix.Length > 0)
                {
                    using (var db = new WordContext())
                    {
                        var count = from findCount in db.Words
                                    select findCount;

                        if (count.Count() == 0)
                        {
                            Console.WriteLine($">{prefix}");
                            Console.WriteLine("Ошибка! Словарь пуст, его необходимо заполнить!");
                        }

                        var query = (from word in db.Words
                                     where word.WordName.StartsWith(prefix.ToLower().Trim())
                                     orderby word.WordFrequency descending, word.WordName
                                     select word).Take(5);

                        if (query.Count() != 0)
                        {
                            Console.WriteLine($">{prefix}");
                            foreach (var addWord in query)
                            {
                                Console.WriteLine("- " + addWord.WordName);
                                prefix = string.Empty;
                            }
                        }
                        else
                        {
                            Console.WriteLine($">{prefix}");
                            Console.WriteLine("Ошибка! Не найдено ни одного значения для автозаполнения!");
                            prefix = string.Empty;
                        }       
                    }
                }
                else
                {
                    prefix += pressKey.KeyChar;
                }    
            }
        }
    }
}
