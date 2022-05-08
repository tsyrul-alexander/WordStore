using WordStore.Manager;

namespace WordStore.Extension {
	internal static class FileDialogExtension {
		public static void UseFileDialogManager(this IServiceCollection services) {
			services.AddSingleton<IFileDialogManager, FileDialogManager>();
		}
	}
}
