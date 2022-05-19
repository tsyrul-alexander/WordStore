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
		public ObservableCollection<BaseDbLookupEntity> Words { get; set; } = new ObservableCollection<BaseDbLookupEntity>();
		public ICommand SelectedWordCommand { get; set; }
		public IWordStorage WordStorage { get; }
		public INavigationManager NavigationManager { get; }

		public WordListViewModel(IWordStorage wordStorage, INavigationManager navigationManager) {
			WordStorage = wordStorage;
			NavigationManager = navigationManager;
			SelectedWordCommand = new Command<BaseDbLookupEntity>(SelectedWord);
		}

		public override void Initialize(IServiceProvider serviceProvider) {
			base.Initialize(serviceProvider);
			LoadWords();
		}
		protected virtual void SelectedWord(BaseDbLookupEntity item) {
			NavigationManager.GoToAsync("word-details", new Dictionary<string, object> {
				{ "wordId", item.Id }
			});
		}
		protected virtual async void LoadWords() {
			Words.Clear();
			var words = await WordStorage.WordRepository.GetListAsync(query => query.LookupOrderBy().Take(WordCount).LookupSelect());
			Words.AddRange(words);
		}
	}
}
