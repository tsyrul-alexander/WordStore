using WordStore.Core.Model;
using WordStore.Data;
using WordStore.Model.View;
using WordStore.ViewModel;

namespace WordStore.Handler {
	public class WordSearchHandler : SearchHandler {
		public IWordStorage WordStorage { get; }
		public WordSearchHandler(IWordStorage wordStorage) {
			WordStorage = wordStorage;
		}
		protected override void OnQueryChanged(string oldValue, string newValue) {
			base.OnQueryChanged(oldValue, newValue);
			if (string.IsNullOrWhiteSpace(newValue)) {
				ItemsSource = null;
				return;
			}
			var words = WordStorage.WordRepository.Get<WordItem, string>(new OrderQueryOptions<Word, string> {
				Count = 30,
				OrderBy = QueryUtilities.LookupOrderBy<Word>()
			});
			ItemsSource = words.Select(word => new WordItemView(word.DisplayValue, word));
		}
		public static WordSearchHandler GetWordSearchHandler() {
			return ViewModelLocator.ServiceProvider.GetService<WordSearchHandler>();
		}
	}
}
