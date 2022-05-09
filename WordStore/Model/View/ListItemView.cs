using WordStore.Core.Model;
using WordStore.ViewModel;

namespace WordStore.Model.View {
	public class ListItemView<T> : BaseModel where T : BaseDbLookupEntity {
		private string _value;

		public string Value {
			get => _value;
			set {
				SetPropertyValue(ref _value, value);
				Item.DisplayValue = value;
			}
		}
		public T Item { get; }
		public ListItemView(T item) : this(item.DisplayValue, item) { }
		public ListItemView(string value, T item) {
			Value = value;
			Item = item;
		}
	}
}
