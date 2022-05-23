using System.Linq.Expressions;
using WordStore.Core.Utility;
using Microsoft.EntityFrameworkCore;
using WordStore.Core.Model.Db;

namespace WordStore.Data.EntityFramework {
	internal static class EFUtilities {
		public static IQueryable<TEntity> SetCount<TEntity>(this IQueryable<TEntity> query,
					int? count) where TEntity : BaseEntity {
			if (count != null) {
				query = query.Take(count.Value);
			}
			return query;
		}
		public static IQueryable<TEntity> SetFilter<TEntity>(this IQueryable<TEntity> query,
				Expression<Func<TEntity, bool>>? filter) where TEntity : BaseEntity {
			if (filter != null) {
				query = query.Where(filter);
			}
			return query;
		}
		public static IQueryable<TEntity> SetIncludeProperties<TEntity>(this IQueryable<TEntity> query,
				string[]? includeProperties) where TEntity : BaseEntity {
			if (includeProperties != null) {
				includeProperties.Foreach(prop => query = query.Include(prop));
			}
			return query;
		}
		public static IQueryable<TEntity> SetOrderBy<TEntity, TKey>(this IQueryable<TEntity> query,
				Expression<Func<TEntity, TKey>>? orderBy, bool descending) where TEntity : BaseEntity {
			if (orderBy != null) {
				if (descending) {
					query = query.OrderByDescending(orderBy);
				} else {
					query = query.OrderBy(orderBy);
				}
			}
			return query;
		}
	}
}
