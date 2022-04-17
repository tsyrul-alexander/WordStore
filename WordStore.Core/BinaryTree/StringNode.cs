namespace WordStore.Core.BinaryTree {
	public class StringNode<TData> : BaseNode<TData, string> {
		protected override BaseNode<TData, string> CreateNode(TData data, string value) {
			return new StringNode<TData> { Data = data, Value = value };
		}
		protected override bool GetIsGreated(string value1, string value2) {
			foreach (var char1 in value1) {
				foreach (var char2 in value2) {
					if (char1.Equals(char2)) {
						continue;
					}
					return char1 > char2;
				}
			}
			return false;
		}
	}
}
