using WordStore.Core.Model;

namespace WordStore.Data {
	public static class QueryUtilities {
		public static IQueryable<TEntity> FilterById<TEntity>(this IQueryable<TEntity> query, Guid id) where TEntity : BaseEntity {
			return query.Where(entity => entity.Id == id);
		}
		public static IQueryable<TEntity> LookupOrderBy<TEntity>(this IQueryable<TEntity> query) where TEntity : BaseLookupEntity {
			return query.OrderBy(entity => entity.DisplayValue);
		}
		public static IQueryable<BaseLookupEntity> LookupSelect<TEntity>(this IQueryable<TEntity> query) where TEntity : BaseLookupEntity {
			return query.Select(entity => new BaseLookupEntity(entity.Id, entity.DisplayValue));
		}
	}
}
