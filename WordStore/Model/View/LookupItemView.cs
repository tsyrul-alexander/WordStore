using WordStore.Core.Model;
using WordStore.Core.Model.Db;

namespace WordStore.Model.View {
	public class LookupItemView<T> : BaseModel where T : BaseLookupEntity {
		private T item;

		public string Value {
			get => Item.DisplayValue;
			set {
				string _value = string.Empty;
				SetPropertyValue(ref _value, value);
				Item.DisplayValue = value;
			}
		}
		public T Item { get => item; set => SetPropertyValue(ref item, value); }
		public LookupItemView(T item) {
			Item = item;
		}
	}
}
