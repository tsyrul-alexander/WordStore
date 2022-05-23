using WordStore.Core.Model.Db;

namespace WordStore.Model.View {
	public class BookItemView {
		public BaseLookupEntity BookItem { get; set; }
		public byte[] Image { get; set; }
		public int Count { get; set; }

		public BookItemView(BaseLookupEntity bookItem, int count, byte[] image = null) {
			BookItem = bookItem;
			Count = count;
			Image = image;
		}
	}
}
