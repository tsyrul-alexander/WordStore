using WordStore.Model.EventArgs;

namespace WordStore.ViewModel {
	public class MainViewModel : BaseViewModel {
		private ShellItem currentShellItem;
		private bool activityIndicatorIsRunning;

		public ShellItem CurrentShellItem {
			get => currentShellItem;
			set => SetPropertyValue(ref currentShellItem, value, OnCurrentShellItemChanged);
		}
		public bool ActivityIndicatorIsRunning {
			get => activityIndicatorIsRunning;
			set => SetPropertyValue(ref activityIndicatorIsRunning, value);
		}

		public virtual void OnCurrentShellItemChanged(ShellItem item) {
			SendMessage("ShellTabChanged", GetShellTabChangedEventArgs(item.CurrentItem));
			ActivityIndicatorIsRunning = !ActivityIndicatorIsRunning;
		}
		protected virtual ShellTabChangedEventArgs GetShellTabChangedEventArgs(ShellItem item) {
			return new ShellTabChangedEventArgs {
				Title = item.Title,
				ViewModelClassType = item.BindingContext.GetType()
			};
		}
	}
}
