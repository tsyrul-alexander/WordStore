using WordStore.Core.Model;

namespace WordStore.Data {
	public static class QueryUtilities {
		public static IQueryable<TEntity> LookupOrderBy<TEntity>(this IQueryable<TEntity> query) where TEntity : BaseDbLookupEntity {
			return query.OrderBy(entity => entity.DisplayValue);
		}
		public static IQueryable<BaseDbLookupEntity> LookupSelect<TEntity>(this IQueryable<TEntity> query) where TEntity : BaseDbLookupEntity {
			return query.Select(entity => new BaseDbLookupEntity(entity.Id, entity.DisplayValue));
		}
	}
}
