using WordStore.ViewModel;

namespace WordStore.View;

public partial class AddWordView : Microsoft.Maui.Controls.ContentView {
	public AddWordView() {
		InitializeComponent();
		var viewModel = (BaseViewModel)BindingContext;
		viewModel.PropertyChanged += ViewModel_PropertyChanged;
	}
	private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
		var viewModel = (AddWordViewModel)sender;
		if (e.PropertyName == nameof(viewModel.AddWordView)) {
			if (viewModel.AddWordView == null) {
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