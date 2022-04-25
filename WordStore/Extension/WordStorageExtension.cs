using Microsoft.EntityFrameworkCore;
using WordStore.Data;
using WordStore.Data.EntityFramework;

namespace WordStore.Extension {
	public static class WordStorageExtension {
		public static void UseWordStorage(this ServiceCollection services) {
			
			services.AddSingleton<IWordStorage, WordEFStorage>((provider) => {
				var optionsBuilder = new DbContextOptionsBuilder<WordDbContext>();
				var appSettings = provider.GetService<AppSettings>();
				var connectionString = appSettings.WordStorageDbConnectionString;
				var options = optionsBuilder.UseSqlite(connectionString).Options;
				return new WordEFStorage(new WordDbContext(options));
			});
		}
	}
}
