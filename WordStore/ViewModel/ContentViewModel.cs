using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Manager;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class ContentViewModel : BaseViewModel {
		private bool isEditMode;
		private string text;

		public bool IsEditMode { get => isEditMode; set => SetPropertyValue(ref isEditMode, value, EditModeChanged); }
		public string Text { get => text; set => SetPropertyValue(ref text, value); }
		public ObservableCollection<LineWordView> Content { get; set; } = new ObservableCollection<LineWordView>();
		public ICommand OpenFileCommand { get; set; }
		public ICommand SaveFileCommand { get; set; }
		public ICommand WordSelectedCommand { get; set; }
		public IFileManager FileManager { get; }
		public IPaginationManager PaginationManager { get; }
		public IWordManager WordManager { get; }
		public IFileDialogManager FileDialogManager { get; }
		public INavigationManager NavigationManager { get; }
		public IWordStorage WordStorage { get; }
		public AppConstants AppConstants { get; }

		public ContentViewModel(IFileManager fileManager, IPaginationManager paginationManager,
				IWordManager wordManager, IFileDialogManager fileDialogManager, INavigationManager navigationManager,
				IWordStorage wordStorage, AppConstants appConstants) {
			FileManager = fileManager;
			PaginationManager = paginationManager;
			WordManager = wordManager;
			FileDialogManager = fileDialogManager;
			NavigationManager = navigationManager;
			WordStorage = wordStorage;
			AppConstants = appConstants;
			OpenFileCommand = new Command(OpenFile);
			SaveFileCommand = new Command(SaveFile);
			WordSelectedCommand = new Command<WordItemView>(WordSelected);
			paginationManager.Changed += PaginationManager_Changed;
		}

		protected virtual void WordSelected(WordItemView wordItemView) {
			Word word;
			if (wordItemView.WordItem == null) {
				word = new Word{ DisplayValue = wordItemView.Value };
			} else {
				word = WordStorage.GetWord(wordItemView.WordItem.Id);
			}
			NavigationManager.GoToAsync("word-details", new Dictionary<string, object>{
				{ "word", word }
			});
		}

		protected virtual void EditModeChanged(bool isEdit) {
			if (isEdit && !string.IsNullOrEmpty(Text)) {
				var lines = Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
				PaginationManager.SetContent(lines);
			}
		}
		protected virtual void SaveFile() {
			//todo
		}
		protected virtual async void OpenFile() {
			var filePath = await FileDialogManager.ShowFileDialogAsync(AppConstants.TxtFileType);
			if (filePath == null) {
				return;
			}
			var textLines = FileManager.ReadAllLines(filePath);
			Text = string.Join(Environment.NewLine, textLines);
		}
		protected virtual void PaginationManager_Changed(IPaginationManager arg1, EventArgs arg2) {
			Content.Clear();
			PrepareLines(arg1.GetCurrentLines());
		}
		protected virtual void PrepareLines(string[] lines) {
			lines.Foreach(PrepareText);
		}
		protected virtual void PrepareText(string text) {
			var line = new LineWordView();
			line.Words.AddRange(WordManager.GetWords(text));
			line.Number = Content.Count;
			Content.Add(line);
			
		}
	}
}
