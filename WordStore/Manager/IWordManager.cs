using WordStore.Model.View;

namespace WordStore.Manager {
	public interface IWordManager {
		IEnumerable<WordItemView> GetWords(string text);
	}
}