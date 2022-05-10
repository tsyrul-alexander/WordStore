using System.Linq.Expressions;
using WordStore.Core.Model.Db;

namespace WordStore.Data {
	public class OrderQueryOptions<TEntity, TKey> : QueryOptions<TEntity> where TEntity : BaseDbEntity {
		public Expression<Func<TEntity, TKey>> OrderBy { get; set; }
		public bool Descending { get; set; } = false;
	}
}
