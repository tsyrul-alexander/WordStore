using WordStore.Core.Model;

namespace WordStore.Model.View {
	public class WordItemView {
		public string Value{ get; set; }
		public WordItem WordItem { get; set; }
		public WordItemView(string value, WordItem wordItem = null) {
			Value = value;
			WordItem = wordItem;
		}
	}
}
