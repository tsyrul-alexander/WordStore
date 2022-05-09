namespace WordStore.Manager {
	internal class DialogManager : IDialogManager {
		public Task<string> DisplayPromptAsync(string title, string message, string accept = "OK",
				string cancel = "Cancel", string placeholder = null, int maxLength = -1,
				Keyboard keyboard = null, string initialValue = "") {
			return Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength,
				keyboard, initialValue);
		}

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
