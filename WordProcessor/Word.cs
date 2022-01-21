using System.Data.Entity;

namespace WordProcessor
{
    // Формирование всего одной таблицы для хранения слов и частоты его использования в загружаемом тексте
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
