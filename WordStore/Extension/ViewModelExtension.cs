using WordStore.ViewModel;

namespace WordStore.Extension {
    internal static class ViewModelExtension {
        public static void UseViewModel(this IServiceCollection services) {
            services.AddTransient<MainViewModel>();
            services.AddTransient<ContentViewModel>();
            services.AddTransient<WordListViewModel>();
            services.AddTransient<WordDetailViewModel>();
            services.AddTransient<AddWordViewModel>();
        }
    }
}
