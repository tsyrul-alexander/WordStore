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
		protected override async void OnQueryChanged(string oldValue, string newValue) {
			base.OnQueryChanged(oldValue, newValue);
			if (string.IsNullOrWhiteSpace(newValue)) {
				ItemsSource = null;
				return;
			}
			var words = await WordStorage.WordRepository.GetCustomAsync(query => query.Where(word => word.DisplayValue.Contains(newValue))
					.LookupOrderBy().Take(30).LookupSelect());
			ItemsSource = words.Select(word => new WordItemView(word.DisplayValue, WordItemViewType.Word, word));
		}
		public static WordSearchHandler GetWordSearchHandler() {
			return ViewModelLocator.ServiceProvider.GetService<WordSearchHandler>();
		}
	}
}
