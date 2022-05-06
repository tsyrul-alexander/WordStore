using WordStore.Core.Manager;

namespace WordStore.Extension {
	public static class PaginationExtention {
		public static void UsePagination(this IServiceCollection services) {
			services.AddTransient<IPaginationManager, PaginationManager>();
		}
	}
}
