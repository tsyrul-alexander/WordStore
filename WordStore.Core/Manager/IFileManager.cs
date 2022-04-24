namespace WordStore.Core.Manager {
	public interface IFileManager {
		string SelectFile(string path, string filter);
		string[] ReadAllLines(string filePath);
	}
}
