using WordStore.Core.BinaryTree;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Model.View;

namespace WordStore.Manager {
	public class WordManager : IWordManager {
		private StringBinaryTree<WordItem> tree;

		public IWordStorage WordStorage { get; }
		protected StringBinaryTree<WordItem> Tree { 
			get {
				if (tree == null) {
					InitializeWordsTree();
				}
				return tree;
			}
			set { tree = value; }
		}
		public WordManager(IWordStorage wordStorage) {
			WordStorage = wordStorage;
		}
		protected virtual void InitializeWordsTree() {
			Tree = new StringBinaryTree<WordItem>();
			var words = WordStorage.WordRepository.Get<WordItem>();
			words.Foreach(word => Tree.AddNode(word, word.DisplayValue));
		}
		public virtual IEnumerable<WordItemView> GetWords(string text) {
			var list = new List<WordItemView>();
			var worlds = text.Split(new[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < worlds.Length;) {
				var (word, count) = GetWord(worlds[i], worlds[i..]);
				i += count;
				list.Add(word);
			}
			return list;
		}
		protected virtual (WordItemView word, int wordCount) GetWord(string word, string[] nextWords) {//todo
			var findWords = Tree.SearchStartWith(word);
			if (findWords.Count == 0) {
				return (new WordItemView(word), 1);
			}
			foreach (var findWord in findWords.OrderByDescending(w => w.DisplayValue.Length)) {
				if (findWord.DisplayValue == word) { 
					return (new WordItemView(word, findWord), 1);
				}
				var findWordTextArray = findWord.DisplayValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (nextWords.Length < findWordTextArray.Length) {
					continue;
				}
				var isCompared = true;
				for (int i = 0; i < findWordTextArray.Length; i++) {
					if (findWordTextArray[i].ToLower() != nextWords[i].ToLower()) {
						isCompared = false;
					}
				}
				if (isCompared) {
					return (new WordItemView(findWord.DisplayValue, findWord), findWordTextArray.Length);
				}
			}
			return (new WordItemView(word), 1);
		}
	}
}
