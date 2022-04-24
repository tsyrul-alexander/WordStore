using WordStore.Manager;

namespace WordStore.Extension {
	public static class FileExtension {
		public static void UseFileManager(this ServiceCollection services) {
			services.AddSingleton<IFileManager, FileManager>();
		}
		public static void UseMockFileManager(this ServiceCollection services) {
			services.AddSingleton<IFileManager, MockFileManager>();
		}
	}
}
