namespace WordStore.Core.Utility {
	public static class TextUtility {
		public static string[] GetLines(this string text) {
			return text.Split('\n', '\r');
		}
		public static int GetLineCount(this string text) {
			return text.GetLines().Length;
		}
	}
}
