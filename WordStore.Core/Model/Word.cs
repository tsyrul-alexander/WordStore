namespace WordStore.Core.Model {
	public class Word : IWord {
		public string Value { get; }
		public WordType Type { get; }

		public Word(string value, WordType type = WordType.Word) {
			Value = value;
			Type = type;
		}
	}
}
