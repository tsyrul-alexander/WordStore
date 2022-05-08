using System.Collections.ObjectModel;
using WordStore.ViewModel;

namespace WordStore.Model.View {
	public class LineWordView : BaseModel {
		private int number;

		public int Number { get => number; set => SetPropertyValue(ref number, value); }
		public ObservableCollection<WordItemView> Words { get; set; } = new ObservableCollection<WordItemView>();
	}
}
