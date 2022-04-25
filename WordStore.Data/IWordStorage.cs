using WordStore.Core.Model;

namespace WordStore.Data {
	public interface IWordStorage {
		IEnumerable<WordItem> GetWords();
		Word GetWord(Guid id);
	}
}
