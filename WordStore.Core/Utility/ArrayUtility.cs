namespace WordStore.Core.Utility {
	public static class ArrayUtility {
		public static void Foreach<T>(this IEnumerable<T> items, Action<T> action) {
			foreach (var item in items) {
				action(item);
			}
		}
	}
}
