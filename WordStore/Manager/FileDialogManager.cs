namespace WordStore.Manager {
	internal class FileDialogManager : IFileDialogManager {
		public async Task<string> ShowFileDialogAsync(FilePickerFileType types) {
			var options = new PickOptions();
			if (types != null) {
				options.FileTypes = types;
			}
			var result = await FilePicker.PickAsync(options);
			return result?.FullPath;
		}
	}
}
