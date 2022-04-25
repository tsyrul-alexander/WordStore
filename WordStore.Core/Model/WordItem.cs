namespace WordStore.Core.Model {
	public class WordItem : BaseDbLookupEntity {
		public WordItem(Guid? id = null, string displayValue = "") : base(id, displayValue) { }
	}
}
