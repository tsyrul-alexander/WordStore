using WordStore.ViewModel;

namespace WordStore.View;

public partial class WordInfoView : ContentView {
	public WordInfoView() {
		InitializeComponent();
		var viewModel = (BaseViewModel)BindingContext;
		viewModel.PropertyChanged += ViewModel_PropertyChanged;
	}
	private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
		var viewModel = (WordInfoViewModel)sender;
		if (e.PropertyName == nameof(viewModel.WordInfoView)) {
			if (viewModel.WordInfoView == null) {
				await this.FadeTo(0, 500);
				IsVisible = false;
			} else {
				Opacity = 0;
				IsVisible = true;
				await this.FadeTo(1, 500);
			}
		}
	}
}