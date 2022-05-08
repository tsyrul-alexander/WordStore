namespace WordStore.Core.Manager {
	public class FileManager : IFileManager {
		public virtual string[] ReadAllLines(string filePath) {
			return File.ReadAllLines(filePath);
		}
	}
}
