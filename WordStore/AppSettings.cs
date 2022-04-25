namespace WordStore {
	internal class AppSettings {
		public const string WordStorageDbName = "Test.db";
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
	}
}
