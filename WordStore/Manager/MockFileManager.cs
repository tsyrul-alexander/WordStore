namespace WordStore.Manager {
	internal class MockFileManager: FileManager {
		public override string[] ReadAllLines(string filePath) {
			return new[] {
				"Test line 1",
				"Test line 2"
			};
		}
		public override string SelectFile(string path, string filter) {
			return "Virtual path";
		}
	}
}
