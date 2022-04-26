using Microsoft.EntityFrameworkCore;
using WordStore.Core.Model;

namespace WordStore.Data.EntityFramework {
	public class WordEFStorage : IWordStorage {
		public WordDbContext Context { get; }
		public WordEFStorage(WordDbContext context) {
			Context = context;
		}

		public Word GetWord(Guid id) {
			return Context.Words.Include(w => w.Examples).Include(w => w.Translations)
				.FirstOrDefault(w => w.Id == id);
		}

		public IEnumerable<WordItem> GetWords(int count = -1, string search = "") {
			var words = Context.Words;
			IQueryable<Word> query = count > 0 ? words.Take(count) : words;
			query = string.IsNullOrEmpty(search) ? query : query.Where(w => w.DisplayValue.Contains(search));
			return query.OrderBy(w => w.DisplayValue).Cast<WordItem>();
		}
	}
}
