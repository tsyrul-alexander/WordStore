using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Manager;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Model;

namespace WordStore.ViewModel {
	public class ContentViewModel : BaseViewModel {
		public ObservableCollection<IWord> Content { get; set; } = new ObservableCollection<IWord>();
		public ICommand OpenFileCommand { get; set; }
		public IFileManager FileManager { get; }
		public IPaginationManager PaginationManager { get; }

		public ContentViewModel(IFileManager fileManager, IPaginationManager paginationManager) {
			FileManager = fileManager;
			PaginationManager = paginationManager;
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
			PrepareLines(arg1.GetCurrentLines());
		}
		protected virtual void PrepareLines(string[] lines) {
			lines.Foreach(PrepareText);
		}
		protected virtual void PrepareText(string text) {
			var worlds = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			worlds.Foreach(word => Content.Add(new Word(word)));
		}
	}
}
