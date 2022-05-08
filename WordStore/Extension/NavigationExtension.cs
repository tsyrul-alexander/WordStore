using WordStore.Manager;

namespace WordStore.Extension {
	internal static class NavigationExtension {
		public static void UseNavigatioManager(this IServiceCollection services) {
			services.AddSingleton<INavigationManager, NavigationManager>();
		}
	}
}
