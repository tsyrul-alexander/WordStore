using WordStore.Core.Model;

namespace WordStore.Data {
	public static class QueryUtilities {
		public static IQueryable<TEntity> LookupOrderBy<TEntity>(this IQueryable<TEntity> query) where TEntity : BaseDbLookupEntity {
			return query.OrderBy(entity => entity.DisplayValue);
		}
	}
}
