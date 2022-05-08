using WordStore.Core.Manager;

namespace WordStore.Extension {
	public static class FileExtension {
		public static void UseFileManager(this IServiceCollection services) {
			services.AddSingleton<IFileManager, FileManager>();
		}
	}
}
