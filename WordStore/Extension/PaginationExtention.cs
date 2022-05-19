using WordStore.Manager;

namespace WordStore.Extension {
	public static class PaginationExtention {
		public static void UsePagination(this IServiceCollection services) {
			services.AddSingleton<IBookPaginationManager, BookPaginationManager>();
		}
	}
}
