using WordStore.Reader;

namespace WordStore.Extension {
	public static class BookReaderExtension {
		public static void UseBookReader(this IServiceCollection services) {
			services.AddTransient<IEpubReader, EpubReader>();
		}
	}
}
