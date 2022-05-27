using WordStore.Core;
using WordStore.Model.View;

namespace WordStore.Manager {
	public interface IWordManager : IAsyncInitialize {
		IEnumerable<WordItemView> GetWords(string text);
	}
}