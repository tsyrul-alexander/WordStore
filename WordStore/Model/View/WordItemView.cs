using WordStore.Core.Model;

namespace WordStore.Model.View {
	public class WordItemView {
		public string Value{ get; set; }
		public BaseDbLookupEntity WordItem { get; set; }
		public WordItemViewType Type { get; set; }
		public WordItemView(string value, WordItemViewType type = WordItemViewType.Word, BaseDbLookupEntity wordItem = null) {
			Value = value;
			Type = type;
			WordItem = wordItem;
		}
	}
}
