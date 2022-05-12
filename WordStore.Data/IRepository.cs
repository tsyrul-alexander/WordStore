using WordStore.Core.Model.Db;

namespace WordStore.Data {
	public interface IRepository<TEntity> where TEntity : BaseDbEntity {
		IEnumerable<TBase> Get<TBase>(Func<IQueryable<TEntity>, IQueryable<TEntity>> query = null) where TBase : BaseDbEntity;
		TEntity GetById(Guid id, params string[] includeProperties);
		void Insert(TEntity entity);
		void Update(TEntity entity);
		void Delete(Guid id);
	}
}
