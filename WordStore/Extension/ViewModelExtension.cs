using WordStore.ViewModel;

namespace WordStore.Extension {
    internal static class ViewModelExtension {
        public static void UseViewModel(this ServiceCollection services) {
            services.AddTransient<MainViewModel>();
            services.AddTransient<ContentViewModel>();
        }
    }
}
