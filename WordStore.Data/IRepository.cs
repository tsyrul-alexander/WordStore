using System.Linq.Expressions;
using WordStore.Core.Model.Db;

namespace WordStore.Data {
	public interface IRepository<TEntity> where TEntity : BaseDbEntity {
		IEnumerable<TBase> Get<TBase>(QueryOptions<TEntity> options = null) where TBase : BaseDbEntity;
		IEnumerable<TBase> Get<TBase, TKey>(OrderQueryOptions<TEntity, TKey> options = null) where TBase : BaseDbEntity;
		TEntity GetById(Guid id);
		void Insert(TEntity entity);
		void Update(TEntity entity);
		void Delete(Guid id);
	}
}
