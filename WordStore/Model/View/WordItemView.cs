using WordStore.Core.Model;

namespace WordStore.Model.View {
	public class WordItemView {
		public string Value { get; set; }
		public BaseLookupEntity WordItem { get; set; }
		public WordItemViewType Type { get; set; }
		public WordItemView(string value, WordItemViewType type = WordItemViewType.Word, BaseLookupEntity wordItem = null) {
			Value = value;
			Type = type;
			WordItem = wordItem;
		}
	}
}
