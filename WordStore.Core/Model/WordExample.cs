namespace WordStore.Core.Model {
	public class WordExample : BaseDbLookupEntity {
		public Guid WordId{ get; set; }
		public WordExample() : this(Guid.Empty, "") { }
		public WordExample(Guid? id, string displayValue) : base(id, displayValue) { }
	}
}
