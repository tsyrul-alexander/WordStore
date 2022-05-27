using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model;
using WordStore.Model.EventArgs;

namespace WordStore.ViewModel {
	public class WordDetailViewModel : BaseViewModel, IQueryAttributable {
		private string currentSentence;
		private Word word;

		public Word Word { get => word; set => SetPropertyValue(ref word, value); }
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
			text = text.FirstCharToUpper();
			if (string.IsNullOrWhiteSpace(text)) {
				return;
			}
			if (!await GetIsUniqueWord(text)) {
				await DialogManager.DisplayAlertAsync("Word", "An entry with this name already exists");//todo LCZ
				return;
			}
			Word.DisplayValue = text;
			CurrentSentence = text;
			await WordRepository.UpdateAsync(Word, nameof(Word.DisplayValue));
			OnWordUpdated(Word.Id);
		}

		public void ApplyQueryAttributes(IDictionary<string, object> query) {
			var sentence = query.GetValueOrDefault<string>("sentence");
			var wordName = query.GetValueOrDefault<string>("wordName");
			var wordId = query.GetValueOrDefault<Guid>("wordId");
			if (wordName == default && wordId == default) {
				throw new ArgumentException($"{nameof(wordName)} or {nameof(wordId)}");
			}
			if (sentence != default) {
				SetSentence(sentence);
			}
			if (wordId != default) {
				SetWord(wordId);
			} else {
				SetWord(wordName);
			}
		}
		protected virtual void SetSentence(string sentence) {
			CurrentSentence = sentence;
		}
		protected virtual async void SetWord(string wordName) {
			var word = CreateNewWord(wordName.FirstCharToUpper());
			await WordRepository.InsertAsync(word);
			Word = word;
			OnWordInserted(word.Id);
		}
		protected virtual async void SetWord(Guid wordId) {
			Word = await WordRepository.GetAsync(query => query.FilterById(wordId));
		}
		protected virtual Word CreateNewWord(string name) {
			return new Word(Guid.NewGuid(), name);
		}
		protected virtual Task<bool> GetIsUniqueWord(string name) {
			return WordRepository.GetIsUniqueLookup(name, word => word.Id != Word.Id);
		}
		protected virtual void OnWordInserted(Guid wordId) {
			OnWordChanged(wordId, OperationType.Insert);
		}
		protected virtual void OnWordUpdated(Guid wordId) {
			OnWordChanged(wordId, OperationType.Update);
		}
		protected virtual void OnWordChanged(Guid wordId, OperationType operationType) {
			SendMessage("WordChanged", new WordChangedEventArgs(wordId, operationType));
		}
	}
}
