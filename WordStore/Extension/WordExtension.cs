using WordStore.Manager;

namespace WordStore.Extension {
	public static class WordExtension {
		public static void UseWordManager(this IServiceCollection services) {
			services.AddTransient<IWordManager, WordManager>();
		}
	}
}
