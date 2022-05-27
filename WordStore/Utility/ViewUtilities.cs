using WordStore.ViewModel;

namespace WordStore.Utility {
	public static class ViewUtilities {
		public static void InitializeViewModel(this VisualElement visualElement) {
			var viewModel = visualElement.BindingContext as BaseViewModel;
			if (viewModel == null || viewModel.IsInitialized) {
				return;
			}
			viewModel.Initialize();
		}
		public static void SetViewModelActive(this VisualElement visualElement, bool isActive) {
			var viewModel = visualElement.BindingContext as BaseViewModel;
			if (viewModel == null) {
				return;
			}
			viewModel.IsActive = isActive;
		}
	}
}
