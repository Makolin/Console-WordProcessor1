using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using System.Text;

namespace WordProcessor
{
    class AdditionalCommands
    {
        // Метод для проверки пути до файла, необходимо загружать текстовый файл в формате UTF8
        public static string CheckFile()
        {
            string inputText = string.Empty;

            Console.Write("Укажите путь до файла: ");
            string path = Console.ReadLine();

            try
            {
                inputText = File.ReadAllText(path, Encoding.UTF8);
            }
            catch
            {
                Console.WriteLine("Ошибка! Неверно указан путь до файла.");
            }
            return inputText;
        }

        // Метод для преобразования текста в словарь и возвращения данной коллекции 
        public static void SplitText(ref Dictionary<string, int> CurrentDictionary, string inputText)
        {
            char[] SeparationCharacters = new char[] { '.', ';', ',', '!', '?', '(', '(', '\n', '\r', ' ' };
            string[] arrayWords = inputText.ToLower().Split(SeparationCharacters);

            foreach (var word in arrayWords)
            {
                int frequency = default;

                if (CurrentDictionary.TryGetValue(word, out frequency))
                {
                    CurrentDictionary[word]++;
                }
                else
                {
                    if (word.Length >= 3 && word.Length <= 15)
                    {
                        CurrentDictionary.Add(word, 1);
                    }
                }
            }
            CurrentDictionary = CurrentDictionary.Where(t => t.Value >= 3).ToDictionary(k => k.Key, v => v.Value);
        }

        // Метод для получения словаря из базы данных и дополнения его словами из файла
        public static void InsertWords(ref Dictionary<string, int> CurrentDictionary, string inputText)
        {
            CurrentDictionary.Clear();
            using (var db = new WordContext())
            {
                var query = from word in db.Words
                            select word;
                foreach (var currentWord in query)
                {
                    CurrentDictionary.Add(currentWord.WordName, currentWord.WordFrequency);
                }
            }
            SplitText(ref CurrentDictionary, inputText);
        }
    }
}
