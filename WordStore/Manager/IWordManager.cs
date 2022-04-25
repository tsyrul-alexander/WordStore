using WordStore.Core.Model;

namespace WordStore.Manager {
	public interface IWordManager {
		WordItem[] GetWords(string text);
	}
}