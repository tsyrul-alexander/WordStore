namespace WordStore.Core.Model {
	public class WordTranslation : BaseDbLookupEntity {
		public Guid WordId { get; set; }
		public WordTranslation(Guid? id = null, string displayValue = "") : base(id, displayValue) { }
	}
}
