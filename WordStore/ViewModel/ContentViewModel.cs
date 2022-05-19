using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.EventArgs;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class ContentViewModel : BaseViewModel {
		private bool isActive;

		public bool IsActive { get => isActive; set => SetPropertyValue(ref isActive, value, OnActiveChange); }
		public ObservableCollection<LineWordView> Content { get; set; } = new ObservableCollection<LineWordView>();
		public ICommand WordSelectedCommand { get; set; }
		public IBookPaginationManager PaginationManager { get; }
		public IWordManager WordManager { get; }
		public IDialogManager DialogManager { get; }
		public INavigationManager NavigationManager { get; }
		public IWordStorage WordStorage { get; }
		public AppConstants AppConstants { get; }

		public ContentViewModel(IBookPaginationManager paginationManager,
				IWordManager wordManager, IDialogManager dialogManager, INavigationManager navigationManager,
				IWordStorage wordStorage, AppConstants appConstants) {
			PaginationManager = paginationManager;
			WordManager = wordManager;
			DialogManager = dialogManager;
			NavigationManager = navigationManager;
			WordStorage = wordStorage;
			AppConstants = appConstants;
			WordSelectedCommand = new Command<WordItemView>(WordSelected);
		}

		public override void Initialize(IServiceProvider serviceProvider) {
			base.Initialize(serviceProvider);
			PaginationManager.PropertyChanged += PaginationManager_PropertyChanged;
			NavigationManager.Navigated += NavigationManager_Navigate;
			UpdateContent();
		}

		private void NavigationManager_Navigate(INavigationManager _, NavigatedEventArgs args) {
			IsActive = args.RouteName == "//Content/BookReader";
		}

		protected virtual void WordSelected(WordItemView wordItemView) {
			if (wordItemView.Type == WordItemViewType.Char) {
				return;
			}
			if (wordItemView.WordItem == null) {
				AddWord(wordItemView);
			} else {
				EditWord(wordItemView);
			}
		}
		protected virtual void EditWord(WordItemView wordItemView) {
			NavigationManager.GoToAsync("word-details", new Dictionary<string, object>{
				{ "wordId", wordItemView.WordItem.Id }
			});
		}
		protected virtual void AddWord(WordItemView wordItemView) {
			var line = Content.First(line => line.Words.Contains(wordItemView));
			SendMessage("AddWord", new AddWordView {
				WordItemView = wordItemView,
				LineWordView = line
			});
		}
		private void OnActiveChange(bool _) {
			UpdateContent();
		}
		protected virtual void PaginationManager_PropertyChanged(object sender, 
				System.ComponentModel.PropertyChangedEventArgs e) {
			UpdateContent();
		}
		protected virtual void UpdateContent() {
			if (!IsActive) {
				return;
			}
			var lines = PaginationManager.Value.GetLines();
			Content.Clear();
			PrepareLines(lines);
		}
		protected virtual void PrepareLines(string[] lines) {
			lines.Foreach(PrepareText);
		}
		protected virtual void PrepareText(string text) {
			var line = new LineWordView {
				Sentence = text,
				Number = Content.Count
			};
			line.Words.AddRange(WordManager.GetWords(text));
			Content.Add(line);
		}
	}
}
