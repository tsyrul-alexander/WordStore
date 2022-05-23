using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public class WordDetailViewModel : BaseViewModel, IQueryAttributable {
		private string currentSentence;
		private BaseLookupEntity word;

		public BaseLookupEntity Word { get => word; set => SetPropertyValue(ref word, value); }
		public string CurrentSentence { get => currentSentence; set => SetPropertyValue(ref currentSentence, value); }
		public ICommand EditCommand { get; set; }
		public IRepository<Word> WordRepository { get; }
		public IDialogManager DialogManager { get; }

		public WordDetailViewModel(IRepository<Word> wordRepository, IDialogManager dialogManager) {
			WordRepository = wordRepository;
			DialogManager = dialogManager;
			EditCommand = new Command(Edit);
		}

		protected virtual async void Edit() {
			var text = await DialogManager.DisplayPromptAsync("Word", "Name: ", initialValue: Word.DisplayValue);
			if (string.IsNullOrWhiteSpace(text)) {
				return;
			}
			Word.DisplayValue = text;
			CurrentSentence = text;
			//WordRepository.UpdateAsync(Word, nameof(Word.DisplayValue));
		}

		public void ApplyQueryAttributes(IDictionary<string, object> query) {
			var wordId = (Guid)query["wordId"];
			query.TryGetValue("sentence", out var sentenceStr);
			var sentence = sentenceStr as string;
			SetWord(wordId, sentence);
		}
		protected virtual async void SetWord(Guid wordId, string sentence) {
			Word = await WordRepository.GetAsync(query => query.FilterById(wordId).LookupSelect());
			CurrentSentence = sentence;
		}
	}
}
