namespace WordStore.ViewModel {
	public class ViewModelLocator {
		public static ServiceProvider ServiceProvider { get; set; }
		public MainViewModel Main => GetViewModel<MainViewModel>();
		public ContentViewModel Content => GetViewModel<ContentViewModel>();
		public WordListViewModel WordList => GetViewModel<WordListViewModel>();
		public WordDetailViewModel WordDetail => GetViewModel<WordDetailViewModel>();

		protected virtual T GetViewModel<T>() where T : BaseViewModel {
			var viewModel = (T)ServiceProvider.GetService<T>();
			viewModel.Initialize();
			return viewModel;
		}
	}
}
