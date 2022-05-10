using Microsoft.EntityFrameworkCore;
using WordStore.Core.Model.Db;

namespace WordStore.Data.EntityFramework {
	public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseDbEntity {
		public WordDbContext Context { get; }
		public DbSet<TEntity> DBSet => Context.Set<TEntity>();

		public EFRepository(WordDbContext context) {
			Context = context;
		}

		public IEnumerable<TBase> Get<TBase>(QueryOptions<TEntity> options = null) where TBase : BaseDbEntity {
			IQueryable<TEntity> query = SetQueryOptions(options);
			return query.Cast<TBase>().ToList();
		}
		public IEnumerable<TBase> Get<TBase, TKey>(OrderQueryOptions<TEntity, TKey> options = null) where TBase : BaseDbEntity {
			IQueryable<TEntity> query = SetQueryOptions(options)
				.SetOrderBy(options?.OrderBy, options?.Descending ?? false);
			return query.Cast<TBase>().ToList();
		}
		public TEntity GetById(Guid id) {
			return DBSet.Find(id);
		}
		public void Insert(TEntity entity) {
			DBSet.Add(entity);
		}
		public void Update(TEntity entity) {
			DBSet.Attach(entity);
			Context.Entry(entity).State = EntityState.Modified;
		}
		public void Delete(Guid id) {
			var entity = GetById(id);
			if (Context.Entry(entity).State == EntityState.Detached) {
				DBSet.Attach(entity);
			}
			DBSet.Remove(entity);
		}
		protected virtual IQueryable<TEntity> SetQueryOptions(QueryOptions<TEntity> options) {
			return DBSet.SetCount(options.Count)
				.SetFilter(options?.Filter)
				.SetIncludeProperties(options?.IncludeProperties);
		}
	}
}
