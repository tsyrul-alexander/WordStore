using Microsoft.EntityFrameworkCore;
using WordStore.Core.Model.Db;
using WordStore.Core.Utility;

namespace WordStore.Data.EntityFramework {
	public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity {
		public WordDbContext Context { get; }
		public DbSet<TEntity> DBSet => Context.Set<TEntity>();

		public EFRepository(WordDbContext context) {
			Context = context;
		}

		public virtual Task<List<TBase>> GetListAsync<TBase>(Func<IQueryable<TEntity>, IQueryable<TBase>> queryFn)
				where TBase : class {
			IQueryable<TBase> query = queryFn(DBSet);
			return query.ToListAsync();
		}
		public virtual Task<TBase?> GetAsync<TBase>(Func<IQueryable<TEntity>, IQueryable<TBase>> queryFn,
				params string[] includeProperties) {
			IQueryable<TBase> query = queryFn(DBSet
				.IgnoreAutoIncludes()
				.SetIncludeProperties(includeProperties));
			return query.FirstOrDefaultAsync();
		}
		public virtual Task<TEntity> GetByIdAsync(Guid id, params string[] includeProperties) {
			return DBSet.SetIncludeProperties(includeProperties)
				.FirstAsync(query => query.Id == id);
		}
		public virtual async Task InsertAsync(params TEntity[] entities) {
			var entries = entities.Select(entity => DBSet.Add(entity)).ToList();
			await Save();
			entries.Foreach(entry => entry.State = EntityState.Detached);
		}
		public virtual async Task UpdateAsync(TEntity entity, params string[] properties) {
			var entry = DBSet.Attach(entity);
			if (properties == null || properties.Length == 0) {
				entry.State = EntityState.Modified;
			} else {
				properties.Foreach(prop => entry.Property(prop).IsModified = true);
			}
			await Save();
			entry.State = EntityState.Detached;
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
		protected virtual Task Save() {
			return Context.SaveChangesAsync();
		}
		protected virtual Task<TEntity?> FindAsync(Guid id) {
			return DBSet.FindAsync(id).AsTask();
		}
	}
}
