using System.Linq.Expressions;
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
		public static async Task<bool> GetIsUniqueLookup<TEntity>(this IRepository<TEntity> repository, string displayValue,
				Expression<Func<TEntity, bool>>? additionFilter = null) where TEntity : BaseLookupEntity {
			var items = await repository.GetListAsync(query => {
				query = query.Where(item => item.DisplayValue == displayValue);
				if (additionFilter != null) {
					query = query.Where(additionFilter);
				}
				return query.Take(1);
			});
			var item = items.FirstOrDefault();
			return item == null;
		}
	}
}
