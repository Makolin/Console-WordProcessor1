using System.Collections.Generic;
using System.Linq;

namespace WordProcessor
{
    class AdditionalCommands
    {
        // Метод для преобразования текста в словарь и возвращения данной коллекции 
        public static void SplitText(ref Dictionary<string, int> CurrentDictionary, string inputText)
        {
            char[] SeparationCharacters = new char[] { '.', ';', ',', '!', '?', '(', '(', '\n', '\r', ' ' };
            string[] arrayWords = inputText.ToLower().Split(SeparationCharacters);

            foreach (var word in arrayWords)
            {
                var frequency = 0;
                if (CurrentDictionary.TryGetValue(word, out frequency))
                {
                    CurrentDictionary[word]++;
                }
                // Имеются ограничения для добавления в словарь по длине и по частоте использования слова по тексту
                else
                {
                    if (word.Length >= 3 && word.Length <= 15)
                        CurrentDictionary.Add(word, 1);
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
