using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public class WordListViewModel : BaseViewModel {
		public const int WordCount = 30;
		private string search;

		public string Search { get => search; set => SetPropertyValue(ref search, value); }
		public ObservableCollection<WordItem> Words { get; set; } = new ObservableCollection<WordItem>();
		public ICommand SelectedWordCommand { get; set; }
		public IWordStorage WordStorage { get; }
		public INavigationManager NavigationManager { get; }

		public WordListViewModel(IWordStorage wordStorage, INavigationManager navigationManager) {
			WordStorage = wordStorage;
			NavigationManager = navigationManager;
			SelectedWordCommand = new Command<WordItem>(SelectedWord);
		}

		public override void Initialize(IServiceProvider serviceProvider) {
			base.Initialize(serviceProvider);
			LoadWords();
		}
		protected virtual void SelectedWord(WordItem item) {
			NavigationManager.GoToAsync("word-details", new Dictionary<string, object> {
				{ "wordId", item.Id }
			});
		}
		protected virtual void LoadWords() {
			Words.Clear();
			var words = WordStorage.WordRepository.Get<WordItem>(query => query.LookupOrderBy().Take(WordCount));
			Words.AddRange(words);
		}
	}
}
