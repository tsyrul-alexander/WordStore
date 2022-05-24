namespace WordStore.Model.EventArgs {
	public class WordChangedEventArgs {
		public Guid WordId { get; set; }
		public OperationType OperationType { get; set; }

		public WordChangedEventArgs(Guid wordId, OperationType operationType) {
			WordId = wordId;
			OperationType = operationType;
		}
	}
}
