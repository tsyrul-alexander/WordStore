using WordStore.Core.Model.Db;

namespace WordStore.Core.Model {
	public class WordExample : BaseLookupEntity {
		private Guid wordId;

		public Guid WordId { get => wordId; set => SetPropertyValue(ref wordId, value); }
		public WordExample() : this(Guid.Empty, "") { }
		public WordExample(Guid? id, string displayValue) : base(id, displayValue) { }
	}
}
