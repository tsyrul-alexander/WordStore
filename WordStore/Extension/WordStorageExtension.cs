using Microsoft.EntityFrameworkCore;
using WordStore.Core.Model;
using WordStore.Data;
using WordStore.Data.EntityFramework;

namespace WordStore.Extension {
	public static class WordStorageExtension {
		public static void UseEFWordStorage(this IServiceCollection services) {
			services.AddSingleton((provider) => {
				var optionsBuilder = new DbContextOptionsBuilder<WordDbContext>();
				var appSettings = provider.GetService<AppSettings>();
				var connectionString = appSettings.WordStorageDbConnectionString;
				var options = optionsBuilder.UseSqlite(connectionString).Options;
				return new WordDbContext(options);
			});
			services.AddSingleton<IWordStorage, WordEFStorage>();
			services.AddSingleton<IRepository<Word>, EFRepository<Word>>();
			services.AddSingleton<IRepository<WordTranslation>, EFRepository<WordTranslation>>();
			services.AddSingleton<IRepository<WordExample>, EFRepository<WordExample>>();
		}
	}
}
