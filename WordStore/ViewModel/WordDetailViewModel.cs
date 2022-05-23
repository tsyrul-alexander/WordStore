using WordStore.Core.Model.Db;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class WordDetailViewModel : BaseViewModel, IQueryAttributable {
		private string currentSentence;
		private LookupItemView<BaseLookupEntity> word;

		public LookupItemView<BaseLookupEntity> Word { get => word; set => SetPropertyValue(ref word, value); }
		public string CurrentSentence { get => currentSentence; set => SetPropertyValue(ref currentSentence, value); }
		public IWordStorage WordStorage { get; }
		public IDialogManager DialogManager { get; }

		public WordDetailViewModel(IWordStorage wordStorage, IDialogManager dialogManager) {
			WordStorage = wordStorage;
			DialogManager = dialogManager;
		}

		public void ApplyQueryAttributes(IDictionary<string, object> query) {
			var wordId = (Guid)query["wordId"];
			query.TryGetValue("sentence", out var sentenceStr);
			var sentence = sentenceStr as string;
			SetWord(wordId, sentence);
		}
		protected virtual async void SetWord(Guid wordId, string sentence) {
			var word = await WordStorage.WordRepository.GetAsync(query => query.FilterById(wordId).LookupSelect());
			Word = GreateLookupItemView(word);
			CurrentSentence = sentence;
		}
	}
}
