namespace WordStore.Core.Model {
	public class Word : WordItem {
		public IList<WordTranslation> Translations { get; set; }
		public IList<WordExample> Examples { get; set; }
		public Word() : this(Guid.Empty, "") { }
		public Word(Guid? id, string displayValue) : base(id, displayValue) { }
	}
}
