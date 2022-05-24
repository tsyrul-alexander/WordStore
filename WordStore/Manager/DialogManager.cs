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
		public virtual Task DisplayAlertAsync(string title, string message, string cancel = "OK") {
			return Application.Current.MainPage.DisplayAlert(title, message, cancel);
		}
		public virtual ActivityIndicatorDialogInfo ShowActivityIndicator() {
			var page = Shell.Current.CurrentPage;
			CommunityToolkit.Maui.Views.Popup popup = null;
			if (Shell.Current.CurrentPage.IsLoaded) {
				popup = ShowActivityIndicatorPopup(page);
				return new ActivityIndicatorDialogInfo(() => HideActivityIndicator(popup));
			}
			var loadedFn = new EventHandler((sender, e) => {
				popup = ShowActivityIndicatorPopup(page);
			});
			page.Loaded += loadedFn;
			return new ActivityIndicatorDialogInfo(() => {
				page.Loaded -= loadedFn;
				if (popup != null) {
					HideActivityIndicator(popup);
				}
			});
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
		protected virtual void HideActivityIndicator(CommunityToolkit.Maui.Views.Popup popup) {
			popup?.Close();
		}
		protected virtual CommunityToolkit.Maui.Views.Popup ShowActivityIndicatorPopup(Page page) {
			var popup = new ActivityIndicatorPopup();
			page.ShowPopup(popup);
			return popup;
		}
	}
}
