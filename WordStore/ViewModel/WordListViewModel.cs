﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Model.Db;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public class WordListViewModel : BaseViewModel {
		public const int WordCount = 30;
		private string search;

		public string Search { get => search; set => SetPropertyValue(ref search, value); }
		public ObservableCollection<BaseLookupEntity> Words { get; set; } = new ObservableCollection<BaseLookupEntity>();
		public ICommand SelectedWordCommand { get; set; }
		public IWordStorage WordStorage { get; }
		public INavigationManager NavigationManager { get; }

		public WordListViewModel(IWordStorage wordStorage, INavigationManager navigationManager) {
			WordStorage = wordStorage;
			NavigationManager = navigationManager;
			SelectedWordCommand = new Command<BaseLookupEntity>(SelectedWord);
		}

		public override void Initialize(IServiceProvider serviceProvider) {
			base.Initialize(serviceProvider);
			LoadWords();
		}
		protected virtual void SelectedWord(BaseLookupEntity item) {
			NavigationManager.GoToAsync("word-details", new Dictionary<string, object> {
				{ "wordId", item.Id }
			});
		}
		protected virtual async void LoadWords() {
			Words.Clear();
			var words = await WordStorage.WordRepository.GetListAsync(query => query.LookupOrderBy().Take(WordCount).LookupSelect());
			Words.AddRange(words);
		}
	}
}
