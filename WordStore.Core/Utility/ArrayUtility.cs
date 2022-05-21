namespace WordStore.Core.Utility {
	public static class ArrayUtility {
		public static void Foreach<T>(this IEnumerable<T> items, Action<T> action) {
			foreach (var item in items) {
				action(item);
			}
		}
		public static void AddRange<T> (this ICollection<T> collection, IEnumerable<T> items) {
			foreach (var item in items) {
				collection.Add(item);
			}
		}
		public static IEnumerable<IEnumerable<T>> Split<T>(this ICollection<T> array, int size) {
			for (var i = 0; i < array.Count / size + 1; i++) {
				yield return array.Skip(i * size).Take(size);
			}
		}
	}
}
