namespace WordStore.Core.BinaryTree {
	public abstract class BaseBinaryTree<TData, TValue, TNode> where TNode : BaseNode<TData, TValue>, new() where TData : class {
		public TNode RootNode { get; set; }
		public virtual TData Search(TValue value) {
			return RootNode?.Search(value);
		}
		public void AddNode(TData data, TValue value) {
			var node = new TNode {
				Data = data,
				Value = value
			};
			if (RootNode == null) {
				RootNode = node;
			} else {
				RootNode.AddNode(data, value);
			}
		}
	}
}
