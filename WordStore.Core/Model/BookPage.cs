using WordStore.Core.Model.Db;

namespace WordStore.Core.Model {
	public class BookPage : BaseDbEntity {
		public Guid BookId { get; set; }
		public string Value { get; set; }
	}
}
