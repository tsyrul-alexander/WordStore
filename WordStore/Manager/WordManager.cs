using WordStore.Core.Model;
using WordStore.Data;

namespace WordStore.Manager {
	public class WordManager : IWordManager {
		public IWordStorage WordStorage { get; }
		public WordManager(IWordStorage wordStorage) {
			WordStorage = wordStorage;
		}
		public virtual WordItem[] GetWords(string text) {
			var worlds = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			return worlds.Select(word => new WordItem{
				DisplayValue = word
			}).ToArray();
		}
	}
}
