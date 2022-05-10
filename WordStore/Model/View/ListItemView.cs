using WordStore.Core.Model;
using WordStore.ViewModel;

namespace WordStore.Model.View {
	public class ListItemView<T> : BaseModel where T : BaseDbLookupEntity {
		public string Value {
			get => Item.DisplayValue;
			set {
				string _value = string.Empty;
				SetPropertyValue(ref _value, value);
				Item.DisplayValue = value;
			}
		}
		public T Item { get; }
		public ListItemView(T item) {
			Item = item;
		}
	}
}
