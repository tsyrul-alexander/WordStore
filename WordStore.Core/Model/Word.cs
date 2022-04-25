namespace WordStore.Core.Model {
	public class Word : WordItem {
		public IList<WordTranslation> Translations { get; set; }
		public IList<WordExample> Examples { get; set; }
		public Word(Guid? id = null, string displayValue = "") : base(id, displayValue) { }
	}
}
