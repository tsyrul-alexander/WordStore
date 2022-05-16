namespace WordStore.Core.Model {
	public class Book : BaseDbLookupEntity {
		public List<BookPage> Pages { get; set; }
	}
}
