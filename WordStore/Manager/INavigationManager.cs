using WordStore.Model.EventArgs;

namespace WordStore.Manager {
	public interface INavigationManager {
		event Action<INavigationManager, NavigatedEventArgs> Navigated;
		Task GoToAsync(string route, Dictionary<string, object> parameters = null);
		Task GoBack();
	}
}
