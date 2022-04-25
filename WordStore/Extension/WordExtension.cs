using WordStore.Manager;

namespace WordStore.Extension {
	public static class WordExtension {
		public static void UseWordManager(this ServiceCollection services) {
			services.AddTransient<IWordManager, WordManager>();
		}
	}
}
