namespace WordStore.ViewModel {
	public class ViewModelLocator {
		public static IServiceProvider ServiceProvider { get; set; }
		public MainViewModel Main => GetViewModel<MainViewModel>();
		public ContentViewModel Content => GetViewModel<ContentViewModel>();
		public WordListViewModel WordList => GetViewModel<WordListViewModel>();
		public WordDetailViewModel WordDetail => GetViewModel<WordDetailViewModel>();
		public AddWordViewModel AddWord => GetViewModel<AddWordViewModel>();
		public BookListViewModel BookList => GetViewModel<BookListViewModel>();
		public BookEditorViewModel BookEditor => GetViewModel<BookEditorViewModel>();
		protected virtual T GetViewModel<T>() where T : BaseViewModel {
			var viewModel = ServiceProvider.GetService<T>();
			viewModel.Initialize(ServiceProvider);
			return viewModel;
		}
	}
}
