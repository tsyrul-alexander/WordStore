using WordStore.Core.Model.Db;

namespace WordStore.Data {
	public interface IRepository<TEntity> where TEntity : BaseDbEntity {
		Task<List<TBase>> GetListAsync<TBase>(Func<IQueryable<TEntity>, IQueryable<TBase>> queryFn) where TBase : class;
		Task<TBase?> GetAsync<TBase>(Func<IQueryable<TEntity>, IQueryable<TBase>> queryFn, params string[] includeProperties);
		Task<TEntity> GetByIdAsync(Guid id, params string[] includeProperties);
		Task InsertAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(Guid id);
	}
}
