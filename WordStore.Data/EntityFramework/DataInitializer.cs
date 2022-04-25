using WordStore.Core.Model;

namespace WordStore.Data.EntityFramework {
	internal class DataInitializer {
		public static void Initialize(WordDbContext context) {
			var word = new Word(Guid.NewGuid(), "in the outskirts");
			var wordTranslation = new WordTranslation(Guid.NewGuid(), "на околицях");
			word.Translations.Add(wordTranslation);
			context.Translations.Add(wordTranslation);
			context.Words.Add(word);
			context.SaveChanges();
		}
	}
}
