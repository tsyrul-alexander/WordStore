﻿using WordStore.Core.BinaryTree;
using WordStore.Core.Model.Db;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Model.View;

namespace WordStore.Manager {
	public class WordManager : IWordManager {
		private StringBinaryTree<BaseLookupEntity> tree;

		public IWordStorage WordStorage { get; }
		protected StringBinaryTree<BaseLookupEntity> Tree { 
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

		protected virtual async void InitializeWordsTree() {
			Tree = new StringBinaryTree<BaseLookupEntity>();
			var words = await WordStorage.WordRepository.GetListAsync(query => query.LookupSelect());
			words.Foreach(word => Tree.AddNode(word, word.DisplayValue));
		}
		public virtual IEnumerable<WordItemView> GetWords(string text) {
			var list = new List<WordItemView>();
			string word = string.Empty;
			int startIndex = 0;
			for (int i = 0; i < text.Length; i++) {
				var currentChar = text[i];
				var isLast = i == text.Length - 1;
				var isLetter = GetIsLetter(currentChar);
				if (isLetter) {
					word += currentChar;
					if (!isLast) {
						continue;
					}
				}
				if (word.Length != 0) {
					var wordView = GetWord(word, startIndex, ref i, text);
					list.Add(wordView);
					word = string.Empty;
					startIndex = (i + 1);
				}
				if (currentChar != ' ' && !isLetter) {
					list.Add(new WordItemView(currentChar.ToString(), WordItemViewType.Char));
				}
			}
			return list;
		}
		protected virtual bool GetIsLetter(char ch) {
			return char.IsLetter(ch);
		}
		protected virtual WordItemView GetWord(string word, int startIndex, ref int index, string text) {//todo
			var findWords = Tree.SearchStartWith(word);
			if (findWords.Count == 0) {
				return new WordItemView(word);
			}
			foreach (var findWord in findWords.OrderByDescending(w => w.DisplayValue.Length)) {
				if (findWord.DisplayValue == word) { 
					return new WordItemView(word, WordItemViewType.Word, findWord);
				}
				var findWordLength = findWord.DisplayValue.Length;
				if (findWordLength > (text.Length - startIndex)) {
					continue;
				}
				if (text[startIndex..(startIndex + findWordLength)] == findWord.DisplayValue) {
					var charCount = (findWordLength - word.Length) - 1;
					index += charCount;
					return new WordItemView(findWord.DisplayValue, WordItemViewType.Word, findWord);
				}
			}
			return new WordItemView(word);
		}
	}
}
