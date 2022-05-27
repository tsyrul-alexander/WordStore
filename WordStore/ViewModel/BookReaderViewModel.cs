using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.EventArgs;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class BookReaderViewModel : BaseViewModel {
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

		public override async void Initialize() {
			await InitializeWordManager();
			base.Initialize();
			UpdateContent();
			PaginationManager.PropertyChanged += PaginationManager_PropertyChanged;
		}
		protected virtual async Task InitializeWordManager() {
			var indicator = DialogManager.ShowActivityIndicator();
			await WordManager.InitializeAsync();
			indicator.Hide();
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
		protected override void OnActiveChange(bool isActive) {
			base.OnActiveChange(isActive);
			UpdateContent();
		}
		protected virtual void PaginationManager_PropertyChanged(object sender, 
				System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(IBookPaginationManager.Value)) {
				UpdateContent();
			}
		}
		protected virtual async void UpdateContent() {
			if (!IsActive || !IsInitialized) {
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
		protected virtual async void OnWordChanged(WordChangedEventArgs args) {
			await WordManager.InitializeAsync();
			UpdateContent();
		}
	}
}
