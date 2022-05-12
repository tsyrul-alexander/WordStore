using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WordStore.Core.Model.Db;

namespace WordStore.Data.EntityFramework {
	public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseDbEntity {
		public WordDbContext Context { get; }
		public DbSet<TEntity> DBSet => Context.Set<TEntity>();

		public EFRepository(WordDbContext context) {
			Context = context;
		}

		public IEnumerable<TBase> Get<TBase>(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFn = null) 
				where TBase : BaseDbEntity {
			IQueryable<TEntity> query = DBSet;
			if (queryFn != null) {
				query = queryFn(query);
			}
			return query.Cast<TBase>().ToList();
		}
		public IEnumerable<TBase> Get<TBase>(QueryOptions<TEntity> options = null) where TBase : BaseDbEntity {
			IQueryable<TEntity> query = SetQueryOptions(options);
			return query.Cast<TBase>().ToList();
		}
		public TEntity GetById(Guid id, params string[] includeProperties) {
			return DBSet.SetIncludeProperties(includeProperties).First(query => query.Id == id);
		}
		public void Insert(TEntity entity) {
			DBSet.Add(entity);
			Save();
		}
		public void Update(TEntity entity) {
			DBSet.Attach(entity);
			Context.Entry(entity).State = EntityState.Modified;
			Save();
		}
		public void Delete(Guid id) {
			var entity = Find(id);
			if (Context.Entry(entity).State == EntityState.Detached) {
				DBSet.Attach(entity);
			}
			DBSet.Remove(entity);
			Save();
		}
		protected virtual IQueryable<TEntity> SetQueryOptions(QueryOptions<TEntity> options) {
			return DBSet.SetCount(options.Count)
				.SetFilter(options?.Filter)
				.SetIncludeProperties(options?.IncludeProperties);
		}
		protected virtual void Save() {
			Context.SaveChanges();
		}
		protected virtual TEntity Find(Guid id) {
			return DBSet.Find(id);
		}
	}
}
