using WordStore.Core.Model;

namespace WordStore.Model.View {
	public class BookItemView {
		public BaseDbLookupEntity BookItem { get; set; }
		public int Count { get; set; }

		public BookItemView(BaseDbLookupEntity bookItem, int count) {
			BookItem = bookItem;
			Count = count;
		}
	}
}
