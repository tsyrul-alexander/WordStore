using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordStore.Core.Model;

namespace WordStore.Data.EntityFramework.EntityConfiguration {
	internal class WordTranslationConfiquration : BaseDbLookupEntityConfiguration<WordTranslation> {
		protected override void ConfiqureDisplayValue(EntityTypeBuilder<WordTranslation> builder) {
			base.ConfiqureDisplayValue(builder);
			builder.Property(o => o.DisplayValue).HasColumnName("Value");
		}
	}
}
