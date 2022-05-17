using CommunityToolkit.Maui.Views;
using WordStore.Model;
using WordStore.Popup;

namespace WordStore.Manager {
	internal class DialogManager : IDialogManager {
		public virtual Task<string> DisplayPromptAsync(string title, string message, string accept = "OK",
				string cancel = "Cancel", string placeholder = null, int maxLength = -1,
				Keyboard keyboard = null, string initialValue = "") {
			return Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength,
				keyboard, initialValue);
		}
		public virtual ActivityIndicatorDialogInfo ShowActivityIndicator() {
			var popup = new ActivityIndicatorPopup();
			Application.Current.MainPage.ShowPopup(popup);
			return new ActivityIndicatorDialogInfo(() => HideActivityIndicator(popup));
		}
		public virtual async Task<string> ShowFileDialogAsync(FilePickerFileType types) {
			var options = new PickOptions();
			if (types != null) {
				options.FileTypes = types;
			}
			var result = await FilePicker.PickAsync(options);
			return result?.FullPath;
		}
		public virtual Task SaveFileDislog(string filePath, string fileName) { 
			return null;
		}
		protected virtual void HideActivityIndicator(ActivityIndicatorPopup popup) {
			popup?.Close();
		}
	}
}
