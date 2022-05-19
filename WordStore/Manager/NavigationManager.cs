using WordStore.Model.EventArgs;

namespace WordStore.Manager {
	internal class NavigationManager : INavigationManager {
		public event Action<INavigationManager, NavigatedEventArgs> Navigated;
		public NavigationManager() {
			Shell.Current.Navigated += Current_Navigated;
		}

		public virtual async Task GoToAsync(string route, Dictionary<string, object> parameters = null) {
			if (parameters == null) {
				await Shell.Current.GoToAsync(route);
			} else {
				await Shell.Current.GoToAsync(route, parameters);
			}
		}
		public virtual async Task GoBack() {
			await GoToAsync("..");
		}
		protected virtual void Current_Navigated(object sender, ShellNavigatedEventArgs e) {
			Navigated?.Invoke(this, new NavigatedEventArgs { RouteName = e.Current.Location.OriginalString });
		}
	}
}
