namespace WordStore.Core.Model {
	public class BookPage : BaseEntity {
		private Guid bookId;
		private int number;
		private string? content;

		public Guid BookId { get => bookId; set => SetPropertyValue(ref bookId, value); }
		public int Number { get => number; set => SetPropertyValue(ref number, value); }
		public string? Content { get => content; set => SetPropertyValue(ref content, value); }
	}
}
