namespace WordStore.Core.Model {
	public class Word : BaseDbLookupEntity {
		public IList<WordTranslation> Translations { get; set; } = new List<WordTranslation>();
		public IList<WordExample> Examples { get; set; } = new List<WordExample>();
		public Word() : this(Guid.Empty, "") { }
		public Word(Guid? id, string displayValue) : base(id, displayValue) { }
	}
}
