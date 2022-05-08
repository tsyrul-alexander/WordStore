using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class WordDetailViewModel : BaseViewModel, IQueryAttributable {
		private WordView word;
		public WordView Word { get => word; set => SetPropertyValue(ref word, value); }
		public ICommand AddTranslation{ get; set; }
		public ICommand EditTranslation { get; set; }
		public ICommand RemoveTranslation { get; set; }

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
