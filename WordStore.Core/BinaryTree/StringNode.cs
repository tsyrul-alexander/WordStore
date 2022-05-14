namespace WordStore.Core.BinaryTree {
	public class StringNode<TData> : BaseNode<TData, string, StringNode<TData>> where TData : class {
		protected override StringNode<TData> CreateNode(TData data, string value) {
			return new StringNode<TData> { Data = data, Value = value };
		}

		public IList<TData> SearchStartWith(string value) {
			var list = new List<TData>();
			SearchStartWith(value, list);
			return list;
		}

		internal virtual void SearchStartWith(string value, IList<TData> items) {
			if (Value == value || Value.StartsWith(value + " ")) {
				items.Add(Data);
			}
			if (GetIsGreated(Value, value)) {
				LeftNode?.SearchStartWith(value, items);
			} else {
				RightNode?.SearchStartWith(value, items);
			}
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
