namespace WordStore.ViewModel {
	public class ViewModelLocator {
		public static IServiceProvider ServiceProvider { get; set; }
		public MainViewModel Main => GetViewModel<MainViewModel>();
		public BookReaderViewModel BookReader => GetViewModel<BookReaderViewModel>();
		public WordListViewModel WordList => GetViewModel<WordListViewModel>();
		public WordDetailViewModel WordDetail => GetViewModel<WordDetailViewModel>();
		public WordInfoViewModel AddWord => GetViewModel<WordInfoViewModel>();
		public BookListViewModel BookList => GetViewModel<BookListViewModel>();
		public BookEditorViewModel BookEditor => GetViewModel<BookEditorViewModel>();
		public TranslationEditListViewModel TranslationEditList => GetViewModel<TranslationEditListViewModel>();
		public ExampleEditListViewModel ExampleEditList => GetViewModel<ExampleEditListViewModel>();
		protected virtual T GetViewModel<T>() where T : BaseViewModel {
			var viewModel = ServiceProvider.GetService<T>();
			viewModel.Initialize(ServiceProvider);
			return viewModel;
		}
	}
}
