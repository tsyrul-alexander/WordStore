using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordStore.Core.Model.Db;

namespace WordStore.Data.EntityFramework.EntityConfiguration {
	internal class BaseDbLookupEntityConfiguration<T> : BaseDbEntityConfiguration<T> where T : BaseLookupEntity {
		public override void Configure(EntityTypeBuilder<T> builder) {
			base.Configure(builder);
		}
		protected virtual void ConfiqureDisplayValue(EntityTypeBuilder<T> builder) {
			builder.Property(p => p.DisplayValue).IsRequired().HasMaxLength(50);
		}
	}
}
