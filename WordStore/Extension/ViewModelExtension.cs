using WordStore.ViewModel;

namespace WordStore.Extension {
    internal static class ViewModelExtension {
        public static void UseViewModel(this IServiceCollection services) {
            services.AddTransient<MainViewModel>();
            services.AddTransient<BookReaderViewModel>();
            services.AddTransient<WordListViewModel>();
            services.AddTransient<WordDetailViewModel>();
            services.AddTransient<WordInfoViewModel>(); 
			services.AddTransient<BookListViewModel>();
		    services.AddTransient<BookEditorViewModel>();
			services.AddTransient<ExampleEditListViewModel>();
			services.AddTransient<TranslationEditListViewModel>();
		}
    }
}
