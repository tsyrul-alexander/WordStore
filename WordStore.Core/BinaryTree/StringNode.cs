namespace WordStore.Core.BinaryTree {
	public class StringNode<TData> : BaseNode<TData, string> where TData : class {
		protected override BaseNode<TData, string> CreateNode(TData data, string value) {
			return new StringNode<TData> { Data = data, Value = value };
		}
		protected override bool GetIsGreated(string value1, string value2) {
			for (int i = 0; i < value1.Length; i++) {
				if (value2.Length <= i) {
					return true;
				}
				var char1 = value1[i];
				var char2 = value2[i];
				if (char1.Equals(char2)) {
					continue;
				}
				return char1 > char2;
			}
			return false;
		}
	}
}
