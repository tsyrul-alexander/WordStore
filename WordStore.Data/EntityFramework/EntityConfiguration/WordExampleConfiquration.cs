using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordStore.Core.Model;

namespace WordStore.Data.EntityFramework.EntityConfiguration {
	internal class WordExampleConfiquration : BaseDbLookupEntityConfiguration<WordExample> {
		protected override void ConfiqureDisplayValue(EntityTypeBuilder<WordExample> builder) {
			base.ConfiqureDisplayValue(builder);
			builder.Property(o => o.DisplayValue).HasColumnName("Value");
		}
	}
}
