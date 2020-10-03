using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace WordProcessor
{
    // Формирование всего одной таблицы для хранения слов после загрузки их из текстового файла
    public class Word
    {
        public int WordId { get; set; }
        public string WordName { get; set; }
        public int WordFrequency { get; set; }
    }

    public class WordContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
    }
}
