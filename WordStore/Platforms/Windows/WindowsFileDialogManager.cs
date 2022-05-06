using WordStore.Manager;

namespace WordStore.Platforms.Windows {
	internal class WindowsFileDialogManager : IFileDialogManager {
		public bool ShowFileDialog(string filter, out string path) {
			//todo
			path = "";
			return true;
		}
	}
}
