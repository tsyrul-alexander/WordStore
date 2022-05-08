namespace WordStore.Manager {
	internal class NavigationManager : INavigationManager {
		public async Task GoToAsync(string route, Dictionary<string, object> parameters = null) {
			if (parameters == null) {
				await Shell.Current.GoToAsync(route);
			} else {
				await Shell.Current.GoToAsync(route, parameters);
			}
		}
		public async Task GoBack() {
			await GoToAsync("..");
		}
	}
}
