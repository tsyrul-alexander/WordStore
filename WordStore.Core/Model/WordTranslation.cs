namespace WordStore.Core.Model {
	public class WordTranslation : BaseDbLookupEntity {
		public Guid WordId { get; set; }
		public WordTranslation() : this(Guid.Empty, "") { }
		public WordTranslation(Guid? id, string displayValue) : base(id, displayValue) { }
	}
}
