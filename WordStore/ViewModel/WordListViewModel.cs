using System.Collections.ObjectModel;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;

namespace WordStore.ViewModel {
	public class WordListViewModel : BaseViewModel {
		public const int WordCount = 30;
		private string search;
		private WordItem selectedWord;

		public string Search { get => search; set => SetPropertyValue(ref search, value); }
		public WordItem SelectedWord { 
			get => selectedWord; set => SetPropertyValue(ref selectedWord, value, OnSelectedWordChanged);
		}
		public ObservableCollection<WordItem> Words { get; set; } = new ObservableCollection<WordItem>();
		public IWordStorage WordStorage { get; }

		public WordListViewModel(IWordStorage wordStorage) {
			WordStorage = wordStorage;
		}
		public override void Initialize() {
			base.Initialize();
			LoaadWords();
		}
		protected virtual void LoaadWords() {
			Words.Clear();
			var words = WordStorage.GetWords(WordCount, Search).ToArray();
			Words.AddRange(words);
		}
		protected virtual void OnSelectedWordChanged(WordItem wordItem) { 
			var word = wordItem == null ? null : WordStorage.GetWord(wordItem.Id);
			SendMessage(word, "ShowWordDetail");
		}
	}
}
