using WordStore.Manager;

namespace WordStore.Extension {
	internal static class DialogExtension {
		public static void UseDialogManager(this IServiceCollection services) {
			services.AddSingleton<IDialogManager, DialogManager>();
		}
	}
}
