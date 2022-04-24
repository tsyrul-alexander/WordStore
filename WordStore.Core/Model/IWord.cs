namespace WordStore.Core.Model {
	public interface IWord {
		string Value { get; }
		WordType Type { get; }
	}
}
