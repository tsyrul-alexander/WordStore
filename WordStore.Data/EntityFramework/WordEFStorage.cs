using WordStore.Core.Model;

namespace WordStore.Data.EntityFramework {
	public class WordEFStorage : IWordStorage {
		public IRepository<Word> WordRepository { get; }
		public IRepository<WordTranslation> WordTranslationRepository { get; }
		public IRepository<WordExample> WordExampleRepository { get; }
		public IRepository<Book> BookRepository { get; }
		public IRepository<BookPage> BookPageRepository { get; }

		public WordEFStorage(IRepository<Word> wordRepository,
				IRepository<WordTranslation> wordTranslationRepository,
				IRepository<WordExample> wordExampleRepository,
				IRepository<Book> bookRepository,
				IRepository<BookPage> bookPageRepository) {
			WordRepository = wordRepository;
			WordTranslationRepository = wordTranslationRepository;
			WordExampleRepository = wordExampleRepository;
			BookRepository = bookRepository;
			BookPageRepository = bookPageRepository;
		}
	}
}
