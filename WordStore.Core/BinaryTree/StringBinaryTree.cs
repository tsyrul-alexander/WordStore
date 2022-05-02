namespace WordStore.Core.BinaryTree {
	public class StringBinaryTree<TData> : BaseBinaryTree<TData, string, StringNode<TData>> where TData : class {
		public IList<TData> SearchStartWith(string text) {
			return RootNode.SearchStartWith(text);
		}
	}
}
