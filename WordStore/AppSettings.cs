using Microsoft.Extensions.Configuration;

namespace WordStore {
	public class AppSettings {
		public string WordStorageDbName => GetValue<string>("App:WordStorageDbName");
		public string WordStorageDbPath {
			get {
				var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				return Path.Combine(basePath, WordStorageDbName);
			}
		}
		public string WordStorageDbConnectionString {
			get {
				return $"Data Source={WordStorageDbPath}";
			}
		}
		public int MaxPageLineSize => GetValue<int>("App:MaxPageLineSize");
		public IConfiguration Configuration { get; }

		public AppSettings(IConfiguration configuration) {
			Configuration = configuration;
		}

		protected virtual T GetValue<T>(string name) {
			return Configuration.GetValue<T>(name);
		}
	}
}
