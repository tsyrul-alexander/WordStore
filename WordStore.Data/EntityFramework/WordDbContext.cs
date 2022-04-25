using Microsoft.EntityFrameworkCore;
using WordStore.Core.Model;
using WordStore.Data.EntityFramework.EntityConfiguration;

namespace WordStore.Data.EntityFramework {
	public class WordDbContext : DbContext {
		public DbSet<Word> Words { get; set; }
		public DbSet<WordTranslation> Translations { get; set; }
		public DbSet<WordExample> Examples { get; set; }
		public WordDbContext(DbContextOptions<WordDbContext> options) : base(options) {
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.ApplyConfiguration(new WordConfiguration());
			modelBuilder.ApplyConfiguration(new WordTranslationConfiquration());
			modelBuilder.ApplyConfiguration(new WordExampleConfiquration());
			DataInitializer.Initialize(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}
	}
}
