using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class WordDetailViewModel : BaseViewModel, IQueryAttributable {
		private WordView word;
		public WordView Word { get => word; set => SetPropertyValue(ref word, value); }
		public ICommand AddTranslationCommand { get; set; }
		public ICommand EditTranslationCommand { get; set; }
		public ICommand DeleteTranslationCommand { get; set; }
		public IWordStorage WordStorage { get; }
		public IDialogManager DialogManager { get; }

		public WordDetailViewModel(IWordStorage wordStorage, IDialogManager dialogManager) {
			AddTranslationCommand = new Command(AddTranslation);
			EditTranslationCommand = new Command<ListItemView<WordTranslation>>(EditTranslation);
			DeleteTranslationCommand = new Command<ListItemView<WordTranslation>>(DeleteTranslation);
			WordStorage = wordStorage;
			DialogManager = dialogManager;
		}

		public void ApplyQueryAttributes(IDictionary<string, object> query) {
			var word = (Guid)query["wordId"];
			SetWord(word);
		}
		protected virtual void DeleteTranslation(ListItemView<WordTranslation> translationView) {
			Word.Translations.Remove(translationView);
		}
		protected virtual async void AddTranslation() {
			var text = await DialogManager.DisplayPromptAsync("Translation", "Enter translation");
			if (string.IsNullOrEmpty(text)) {
				return;
			}
			Word.Translations.Add(new ListItemView<WordTranslation>(new WordTranslation(Guid.NewGuid(), text)));
		}
		protected virtual async void EditTranslation(ListItemView<WordTranslation> translationView) {
			var text = await DialogManager.DisplayPromptAsync("Translation", "Enter translation", initialValue: translationView.Value);
			if (string.IsNullOrEmpty(text)) {
				return;
			}
			translationView.Value = text;
		}
		protected virtual async void SetWord(Guid wordId) {
			Word word = await WordStorage.WordRepository.GetByIdAsync(wordId, nameof(Word.Translations), nameof(Word.Examples));
			Word = new WordView(word);
		}
	}
}
