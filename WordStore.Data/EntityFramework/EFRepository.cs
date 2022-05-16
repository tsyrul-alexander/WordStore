using Microsoft.EntityFrameworkCore;
using WordStore.Core.Model.Db;

namespace WordStore.Data.EntityFramework {
	public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseDbEntity {
		public WordDbContext Context { get; }
		public DbSet<TEntity> DBSet => Context.Set<TEntity>();

		public EFRepository(WordDbContext context) {
			Context = context;
		}

		public virtual Task<List<TBase>> GetCustomAsync<TBase>(Func<IQueryable<TEntity>, IQueryable<TBase>> queryFn)
				where TBase : class {
			IQueryable<TBase> query = queryFn(DBSet);
			return query.AsNoTracking().ToListAsync();
		}
		public virtual Task<List<TEntity>> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? queryFn = null) {
			IQueryable<TEntity> query = DBSet;
			if (queryFn != null) {
				query = queryFn(query);
			}
			return query.AsNoTracking().ToListAsync();
		}
		public virtual Task<TEntity> GetByIdAsync(Guid id, params string[] includeProperties) {
			return DBSet.SetIncludeProperties(includeProperties).AsNoTracking()
				.FirstAsync(query => query.Id == id);
		}
		public virtual async Task InsertAsync(TEntity entity) {
			DBSet.Add(entity);
			await Save();
		}
		public virtual async Task UpdateAsync(TEntity entity) {
			DBSet.Attach(entity);
			Context.Entry(entity).State = EntityState.Modified;
			await Save();
		}
		public virtual async Task DeleteAsync(Guid id) {
			var entity = await FindAsync(id);
			if (entity == null) {
				throw new NullReferenceException(nameof(id));
			}
			if (Context.Entry(entity).State == EntityState.Detached) {
				DBSet.Attach(entity);
			}
			DBSet.Remove(entity);
			await Save();
		}
		protected virtual IQueryable<TEntity> SetQueryOptions(QueryOptions<TEntity> options) {
			return DBSet.SetCount(options.Count)
				.SetFilter(options?.Filter)
				.SetIncludeProperties(options?.IncludeProperties);
		}
		protected virtual Task Save() {
			return Context.SaveChangesAsync();
		}
		protected virtual Task<TEntity?> FindAsync(Guid id) {
			return DBSet.FindAsync(id).AsTask();
		}
	}
}
