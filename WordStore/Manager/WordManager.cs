using WordStore.Core.Model;

namespace WordStore.Manager {
	public class WordManager : IWordManager {

		public virtual WordItem[] GetWords(string text) {
			var worlds = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			return worlds.Select(word => new WordItem{
				DisplayValue = word
			}).ToArray();
		}
	}
}
