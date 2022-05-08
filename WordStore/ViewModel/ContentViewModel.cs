using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Manager;
using WordStore.Core.Utility;
using WordStore.Manager;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class ContentViewModel : BaseViewModel {
		public ObservableCollection<LineWordView> Content { get; set; } = new ObservableCollection<LineWordView>();
		public ICommand OpenFileCommand { get; set; }
		public ICommand SaveFileCommand { get; set; }
		public IFileManager FileManager { get; }
		public IPaginationManager PaginationManager { get; }
		public IWordManager WordManager { get; }
		public IFileDialogManager FileDialogManager { get; }
		public AppConstants AppConstants { get; }
		public string FilePath { get; set; }

		public ContentViewModel(IFileManager fileManager, IPaginationManager paginationManager,
				IWordManager wordManager, IFileDialogManager fileDialogManager, AppConstants appConstants) {
			FileManager = fileManager;
			PaginationManager = paginationManager;
			WordManager = wordManager;
			FileDialogManager = fileDialogManager;
			AppConstants = appConstants;
			OpenFileCommand = new Command(OpenFile);
			SaveFileCommand = new Command(SaveFile);
			paginationManager.Changed += PaginationManager_Changed;
		}

		private void SaveFile() {
			
		}

		public async virtual void OpenFile() {
			FilePath = await FileDialogManager.ShowFileDialogAsync(AppConstants.TxtFileType);
			if (FilePath == null) {
				return;
			}
			var textLines = FileManager.ReadAllLines(FilePath);
			PaginationManager.SetContent(textLines);
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
