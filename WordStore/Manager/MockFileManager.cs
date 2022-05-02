using WordStore.Core.Manager;

namespace WordStore.Manager {
	internal class MockFileManager: FileManager {
		public override string[] ReadAllLines(string filePath) {
			return new[] {
				"Test line 1",
				"Test line 2",
				"I heard he could appear and disappear at whim.",
				"Today it's not like that - today the privileged class lives in the outskirts."
			};
		}
		public override string SelectFile(string path, string filter) {
			return "Virtual path";
		}
	}
}
