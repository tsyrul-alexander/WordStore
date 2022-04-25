using WordStore.Core.Manager;

namespace WordStore.Extension {
	public static class PaginationExtention {
		public static void UsePagination(this ServiceCollection services) {
			services.AddTransient<IPaginationManager, PaginationManager>();
		}
	}
}
