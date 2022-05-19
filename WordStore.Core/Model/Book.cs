namespace WordStore.Core.Model {
	public class Book : BaseDbLookupEntity {
		public int PageNumber { get; set; }
		public List<BookPage> Pages { get; set; }
	}
}
