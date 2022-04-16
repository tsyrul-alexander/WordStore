using System.Collections.ObjectModel;

namespace WordStore.ViewModel {
	public class ContentViewModel : BaseViewModel {
		public ObservableCollection<string> Lines { get; set; } = new ObservableCollection<string>();
	
	}
}
