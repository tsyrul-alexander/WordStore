using WordStore.Core.Model;

namespace WordStore.Data.EntityFramework {
	public class WordEFStorage : IWordStorage {
		public WordDbContext Context { get; }
		public WordEFStorage(WordDbContext context) {
			Context = context;
		}

		public Word GetWord(Guid id) {
			return Context.Words.FirstOrDefault(w => w.Id == id);
		}

		public IEnumerable<WordItem> GetWords() {
			return Context.Words.Cast<WordItem>();//todo optimization
		}
	}
}
