namespace WordStore.Manager {
	public interface INavigationManager {
		Task GoToAsync(string route, Dictionary<string, object> parameters = null);
		Task GoBack();
	}
}
