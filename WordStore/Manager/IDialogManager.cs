using WordStore.Model;

namespace WordStore.Manager {
	public interface IDialogManager {
		Task<string> ShowFileDialogAsync(FilePickerFileType types = null);
		Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel",
				string placeholder = null, int maxLength = -1, Keyboard keyboard = null, string initialValue = "");
		Task DisplayAlertAsync(string title, string message, string cancel = "OK");
		ActivityIndicatorDialogInfo ShowActivityIndicator();
	}
}
