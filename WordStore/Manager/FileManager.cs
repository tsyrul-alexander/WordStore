namespace WordStore.Manager {
	public class FileManager : IFileManager {
		public virtual string[] ReadAllLines(string filePath) {
			return File.ReadAllLines(filePath);
		}

		public virtual string SelectFile(string path, string filter) {
			throw new NotImplementedException();
		}
	}
}
