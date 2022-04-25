using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordStore.Core.Model.Db;

namespace WordStore.Data.EntityFramework.EntityConfiguration {
	internal class BaseDbEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseDbEntity {
		public virtual void Configure(EntityTypeBuilder<T> builder) {
			ConfiqureKey(builder);
		}
		protected virtual void ConfiqureKey(EntityTypeBuilder<T> builder) {
			builder.HasKey(c => c.Id);
		}
	}
}
