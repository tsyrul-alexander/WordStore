using System.Linq.Expressions;
using WordStore.Core.Model.Db;

namespace WordStore.Data {
	public class QueryOptions<TEntity> where TEntity : BaseDbEntity {
		public int Count { get; set; }
		public Expression<Func<TEntity, bool>> Filter { get; set; }
		public string[] IncludeProperties { get; set; }
	}
}
