namespace WordStore.Core.Manager {
	public interface IFileManager {
		string[] ReadAllLines(string filePath);
	}
}
