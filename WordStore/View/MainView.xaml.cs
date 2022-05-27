using WordStore.Utility;

namespace WordStore.View;

public partial class MainView : Shell {
	public MainView() {
		InitializeComponent();
		this.InitializeViewModel();
		Navigating += MainView_Navigating;
		Navigated += MainView_Navigated;
	}

	private void MainView_Navigating(object sender, ShellNavigatingEventArgs e) {
		Current.CurrentPage.SetViewModelActive(false);
	}

	protected virtual void MainView_Navigated(object sender, ShellNavigatedEventArgs e) {
		Current.CurrentPage.InitializeViewModel();
		Current.CurrentPage.SetViewModelActive(true);
	}
}