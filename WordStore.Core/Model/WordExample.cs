namespace WordStore.Core.Model {
	public class WordExample : BaseDbLookupEntity {
		public Guid WordId{ get; set; }
		public WordExample(Guid? id = null, string displayValue = "") : base(id, displayValue) { }
	}
}
