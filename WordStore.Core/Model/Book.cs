using WordStore.Core.Model.Db;

namespace WordStore.Core.Model {
	public class Book : BaseLookupEntity {
		private int pageNumber = 1;
		private byte[]? image;

		public int PageNumber { get => pageNumber; set => SetPropertyValue(ref pageNumber, value); }
		public byte[]? Image { get => image; set => SetPropertyValue(ref image, value); }
		public List<BookPage>? Pages { get; set; }
	}
}
