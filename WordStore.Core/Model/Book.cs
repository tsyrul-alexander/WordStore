namespace WordStore.Core.Model {
	public class Book : BaseDbLookupEntity {
		public int PageNumber { get; set; } = 1;
		public byte[]? Image { get; set; }
		public List<BookPage>? Pages { get; set; }
	}
}
