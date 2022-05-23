namespace WordStore.Core.Model {
	public class WordTranslation : BaseLookupEntity {
		private Guid wordId;

		public Guid WordId { get => wordId; set => SetPropertyValue(ref wordId, value); }
		public WordTranslation() : this(Guid.Empty, "") { }
		public WordTranslation(Guid? id, string displayValue) : base(id, displayValue) { }
	}
}
