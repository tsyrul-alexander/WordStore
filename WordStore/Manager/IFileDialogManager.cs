namespace WordStore.Manager {
	public interface IFileDialogManager {
		Task<string> ShowFileDialogAsync(FilePickerFileType types = null);
	}
}
