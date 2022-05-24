using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.EventArgs;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class BookReaderViewModel : BaseViewModel {
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

		public BookReaderViewModel(IBookPaginationManager paginationManager,
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
		protected virtual void NavigationManager_Navigate(INavigationManager _, NavigatedEventArgs args) {
			IsActive = args.RouteName == "//Content/BookReader";
		}
		protected virtual void WordSelected(WordItemView wordItemView) {
			if (wordItemView.Type == WordItemViewType.Char) {
				return;
			}
			WordInfo(wordItemView);
		}
		protected virtual void WordInfo(WordItemView wordItemView) {
			var line = Content.First(line => line.Words.Contains(wordItemView));
			SendMessage("WordInfo", new WordInfoView {
				WordItemView = wordItemView,
				LineWordView = line
			});
		}
		private void OnActiveChange(bool _) {
			UpdateContent();
		}
		protected virtual void PaginationManager_PropertyChanged(object sender, 
				System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(IBookPaginationManager.Value)) {
				UpdateContent();
			}
		}
		protected virtual async void UpdateContent() {
			if (!IsActive) {
				return;
			}
			var indicator = DialogManager.ShowActivityIndicator();
			var textLines = PaginationManager.Value.GetLines();
			Content.Clear();
			var lines = await PrepareLines(textLines);
			Content.AddRange(lines);
			indicator.Hide();
		}
		protected virtual Task<List<LineWordView>> PrepareLines(string[] lines) {
			return Task.Run(() => {
				return lines.Where(line => !string.IsNullOrWhiteSpace(line))
					.Select((line, i) => PrepareText(line, i + 1)).ToList();
			});
		}
		protected virtual LineWordView PrepareText(string text, int number) {
			var line = new LineWordView {
				Sentence = text,
				Number = number
			};
			line.Words.AddRange(WordManager.GetWords(text));
			return line;
		}
		protected override void SubscribeMessages() {
			base.SubscribeMessages();
			SubscribeMessage<WordChangedEventArgs>("WordChanged", OnWordChanged);
		}
		protected override void UnsubscribeMessages() {
			base.UnsubscribeMessages();
			UnsubscribeMessage<WordChangedEventArgs>("WordChanged");
		}
		protected virtual void OnWordChanged(WordChangedEventArgs args) {
			UpdateContent();
		}
	}
}
