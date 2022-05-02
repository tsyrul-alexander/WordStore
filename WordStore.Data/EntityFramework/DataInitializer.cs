using Microsoft.EntityFrameworkCore;
using WordStore.Core.Model;

namespace WordStore.Data.EntityFramework {
	internal class DataInitializer {
		public static void Initialize(ModelBuilder modelBuilder) {
			var word = new Word(Guid.NewGuid(), "in the outskirts");
			var wordTranslation = new WordTranslation(Guid.NewGuid(), "на околицях") {
				WordId = word.Id
			};
			var word2 = new Word(Guid.NewGuid(), "appear");
			var wordTranslation2 = new WordTranslation(Guid.NewGuid(), "виявляється") {
				WordId = word2.Id
			};
			modelBuilder.Entity<WordTranslation>().HasData(wordTranslation, wordTranslation2);
			modelBuilder.Entity<Word>().HasData(word, word2);
		}
	}
}
