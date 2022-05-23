using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class WordInfoViewModel : BaseViewModel {
		private WordInfoView wordInfoView;

		public ICommand CloseCommand { get; set; }
		public ICommand OpenDetailCommand { get; set; }
		public ICommand AddCommand { get; set; }
		public WordInfoView WordInfoView { get => wordInfoView; set => SetPropertyValue(ref wordInfoView, value); }
		public ObservableCollection<LookupItemView<WordTranslation>> Translations { get; set; } = new ObservableCollection<LookupItemView<WordTranslation>>();
		public IWordStorage WordStorage { get; }
		public INavigationManager NavigationManager { get; }

		public WordInfoViewModel(IWordStorage wordStorage, INavigationManager navigationManager) {
			WordStorage = wordStorage;
			NavigationManager = navigationManager;
			CloseCommand = new Command(Close);
			OpenDetailCommand = new Command(OpenDetail);
			AddCommand = new Command(AddWord);
		}

		protected override void SubscribeMessages() {
			base.SubscribeMessages();
			SubscribeMessage<WordInfoView>("WordInfo", SetWordInfo);
		}
		protected virtual void OpenDetail() {
			OpenWordDetail(WordInfoView);
		}
		protected virtual async void AddWord() {
			var word = new Word(Guid.NewGuid(), WordInfoView.WordItemView.Value);
			await WordStorage.WordRepository.InsertAsync(word);
			WordInfoView.WordItemView.WordItem = word;
			OpenDetail();
		}
		protected virtual void Close(object obj) {
			WordInfoView = null;
		}
		protected virtual async void SetWordInfo(WordInfoView wordInfoView) {
			Translations.Clear();
			WordInfoView = wordInfoView;
			var wordItem = wordInfoView.WordItemView.WordItem;
			if (wordItem == null) {
				return;
			}
			var translations = await WordStorage.WordTranslationRepository
					.GetListAsync(query => query.Where(tr => tr.WordId == wordItem.Id));
			Translations.AddRange(translations.Select(tr => new LookupItemView<WordTranslation>(tr)));
		}
		protected virtual void OpenWordDetail(WordInfoView info) {
			NavigationManager.GoToAsync("word-details", new Dictionary<string, object>{
				{ "wordId", info.WordItemView.WordItem.Id },
				{ "sentence", info.LineWordView.Sentence }
			});
		}
	}
}
