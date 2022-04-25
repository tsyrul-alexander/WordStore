using System.Collections.ObjectModel;

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
	}
}
