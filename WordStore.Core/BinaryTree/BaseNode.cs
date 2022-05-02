namespace WordStore.Core.BinaryTree {
	public abstract class BaseNode<TData, TValue, TNode> where TData : class where TNode : BaseNode<TData, TValue, TNode> {
		public TNode LeftNode { get; set; }
		public TNode RightNode { get; set; }
		public TData Data { get; set; }
		public TValue Value { get; set; }

		public virtual TData Search(TValue value) {
			if (Value.Equals(value)) {
				return Data;
			}
			if (GetIsGreated(Value, value)) {
				return LeftNode?.Search(value);
			} else {
				return RightNode?.Search(value);
			}
		}

		public virtual void AddNode(TData data, TValue value) {
			if (GetIsGreated(Value, value)) {
				if (LeftNode == null) {
					LeftNode = CreateNode(data, value);
				} else {
					LeftNode.AddNode(data, value);
				}
			} else {
				if (RightNode == null) {
					RightNode = CreateNode(data, value);
				} else {
					RightNode.AddNode(data, value);
				}
			}
		}
		protected abstract TNode CreateNode(TData data, TValue value);
		protected abstract bool GetIsGreated(TValue value1, TValue value2);
	}
}
