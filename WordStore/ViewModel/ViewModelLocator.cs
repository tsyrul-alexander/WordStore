namespace WordStore.ViewModel {
    public class ViewModelLocator {
        public static ServiceProvider ServiceProvider { get; set; }
        public MainViewModel Main => GetViewModel<MainViewModel>();

        protected virtual T GetViewModel<T>() where T : BaseViewModel {
            return (T)ServiceProvider.GetService<T>();
        }
    }
}
