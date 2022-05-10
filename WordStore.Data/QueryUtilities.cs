using System.Linq.Expressions;
using WordStore.Core.Model;

namespace WordStore.Data {
	public static class QueryUtilities {
		public static Expression<Func<TEntity, string>> LookupOrderBy<TEntity>() where TEntity : BaseDbLookupEntity {
			return entity => entity.DisplayValue;
		}
	}
}
