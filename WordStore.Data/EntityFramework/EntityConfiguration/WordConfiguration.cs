using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordStore.Core.Model;

namespace WordStore.Data.EntityFramework.EntityConfiguration {
	internal class WordConfiguration : BaseDbLookupEntityConfiguration<Word> {
		protected override void ConfiqureDisplayValue(EntityTypeBuilder<Word> builder) {
			base.ConfiqureDisplayValue(builder);
			builder.Property(o => o.DisplayValue).HasColumnName("Value");
		}
	}
}
