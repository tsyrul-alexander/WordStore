using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Manager;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public class ContentViewModel : BaseViewModel {
		public ObservableCollection<WordItem> Content { get; set; } = new ObservableCollection<WordItem>();
		public ICommand OpenFileCommand { get; set; }
		public IFileManager FileManager { get; }
		public IPaginationManager PaginationManager { get; }
		public IWordManager WordManager { get; }

		public ContentViewModel(IFileManager fileManager, IPaginationManager paginationManager,
				IWordManager wordManager) {
			FileManager = fileManager;
			PaginationManager = paginationManager;
			WordManager = wordManager;
			OpenFileCommand = new Command(OpenFile);
			paginationManager.Changed += PaginationManager_Changed;
		}

		public virtual void OpenFile() {
			var filePath = FileManager.SelectFile(null, ".txt");
			if (filePath == null) {
				return;
			}
			var textLines = FileManager.ReadAllLines(filePath);
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
			Content.AddRange(WordManager.GetWords(text));
		}
	}
}
