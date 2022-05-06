using WordStore.Core.Manager;
using WordStore.Manager;

namespace WordStore.Extension {
	public static class FileExtension {
		public static void UseFileManager(this IServiceCollection services) {
			services.AddSingleton<IFileManager, FileManager>();
		}
		public static void UseMockFileManager(this IServiceCollection services) {
			services.AddSingleton<IFileManager, MockFileManager>();
		}
	}
}
