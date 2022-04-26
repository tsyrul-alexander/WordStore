using WordStore.ViewModel;

namespace WordStore.Model.View {
	public class ListItemView : BaseModel {
		private string _value;

		public string Value { get => _value; set => SetPropertyValue(ref _value, value); }
		public object Item { get; }

		public ListItemView(string value, object item) {
			Value = value;
			Item = item;
		}
	}
}
