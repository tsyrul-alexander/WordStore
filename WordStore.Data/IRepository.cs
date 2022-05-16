using WordStore.Core.Model.Db;

namespace WordStore.Data {
	public interface IRepository<TEntity> where TEntity : BaseDbEntity {
		Task<List<TBase>> GetCustomAsync<TBase>(Func<IQueryable<TEntity>, IQueryable<TBase>> queryFn) where TBase : class;
		Task<List<TEntity>> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFn = null);
		Task<TEntity> GetByIdAsync(Guid id, params string[] includeProperties);
		Task InsertAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(Guid id);
	}
}
