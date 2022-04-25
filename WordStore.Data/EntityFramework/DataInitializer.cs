using Microsoft.EntityFrameworkCore;
using WordStore.Core.Model;

namespace WordStore.Data.EntityFramework {
	internal class DataInitializer {
		public static void Initialize(ModelBuilder modelBuilder) {
			var word = new Word(Guid.NewGuid(), "in the outskirts");
			var wordTranslation = new WordTranslation(Guid.NewGuid(), "на околицях");
			//word.Translations = new List<WordTranslation>(new [] { wordTranslation });
			wordTranslation.WordId = word.Id;
			modelBuilder.Entity<WordTranslation>().HasData(wordTranslation);
			modelBuilder.Entity<Word>().HasData(word);
		}
	}
}
