namespace WordStore.Manager {
	public interface IFileDialogManager {
		bool ShowFileDialog(string filter, out string path);
	}
}
