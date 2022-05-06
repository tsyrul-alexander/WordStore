using WordStore.Manager;

namespace WordStore.Extension {
	internal static class FileDialogExtension {
		public static void UseFileDialogManager(this IServiceCollection services) {
#if WINDOWS
				services.AddSingleton<IFileDialogManager, WordStore.Platforms.Windows.WindowsFileDialogManager>();
#endif
		}
	}
}
