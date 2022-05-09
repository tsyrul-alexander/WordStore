using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Manager;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class WordDetailViewModel : BaseViewModel, IQueryAttributable {
		private WordView word;
		public WordView Word { get => word; set => SetPropertyValue(ref word, value); }
		public ICommand AddTranslationCommand { get; set; }
		public ICommand EditTranslationCommand { get; set; }
		public ICommand DeleteTranslationCommand { get; set; }
		public IDialogManager DialogManager { get; }

		public WordDetailViewModel(IDialogManager dialogManager) {
			AddTranslationCommand = new Command(AddTranslation);
			EditTranslationCommand = new Command<ListItemView<WordTranslation>>(EditTranslation);
			DeleteTranslationCommand = new Command<ListItemView<WordTranslation>>(DeleteTranslation);
			DialogManager = dialogManager;
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
		protected override void SubscribeMessages() {
			base.SubscribeMessages();
			SubscribeMessage<Word>("ShowWordDetail", SetWord);
		}
		protected virtual void SetWord(Word word) {
			Word = new WordView(word);
		}
		protected override void UnsubscribeMessages() {
			base.UnsubscribeMessages();
			UnsubscribeMessage<Word>("ShowWordDetail");
		}
		public void ApplyQueryAttributes(IDictionary<string, object> query) {
			var word = query["word"] as Word;
			SetWord(word);
		}
	}
}
