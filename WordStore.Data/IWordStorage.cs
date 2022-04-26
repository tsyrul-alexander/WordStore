using WordStore.Core.Model;

namespace WordStore.Data {
	public interface IWordStorage {
		IEnumerable<WordItem> GetWords(int count = -1, string search = "");
		Word GetWord(Guid id);
	}
}
