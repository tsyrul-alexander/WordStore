using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.EventArgs;

namespace WordStore.ViewModel {
	public class WordListViewModel : BaseViewModel {
		public const int WordCount = 30;
		private string search;

		public string Search { get => search; set => SetPropertyValue(ref search, value); }
		public ObservableCollection<BaseLookupEntity> Words { get; set; } = new ObservableCollection<BaseLookupEntity>();
		public ICommand SelectedWordCommand { get; set; }
		public IWordStorage WordStorage { get; }
		public INavigationManager NavigationManager { get; }

		public WordListViewModel(IWordStorage wordStorage, INavigationManager navigationManager) {
			WordStorage = wordStorage;
			NavigationManager = navigationManager;
			SelectedWordCommand = new Command<BaseLookupEntity>(SelectedWord);
		}

		public override void Initialize() {
			base.Initialize();
			LoadWords();
		}
		protected virtual void SelectedWord(BaseLookupEntity item) {
			NavigationManager.GoToAsync("word-details", new Dictionary<string, object> {
				{ "wordId", item.Id }
			});
		}
		protected virtual async void LoadWords() {
			Words.Clear();
			var words = await WordStorage.WordRepository.GetListAsync(query => query.LookupOrderBy().Take(WordCount).LookupSelect());
			Words.AddRange(words);
		}
		protected override void SubscribeMessages() {
			base.SubscribeMessages();
			SubscribeMessage<WordChangedEventArgs>("WordChanged", OnWordChanged);
		}
		protected override void UnsubscribeMessages() {
			base.UnsubscribeMessages();
			UnsubscribeMessage<WordChangedEventArgs>("WordChanged");
		}
		protected virtual void OnWordChanged(WordChangedEventArgs args) {
			LoadWords();
		}
	}
}
