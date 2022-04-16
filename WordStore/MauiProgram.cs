using WordStore.Extension;
using WordStore.ViewModel;

namespace WordStore;

public static class MauiProgram {
	public static MauiApp CreateMauiApp() {
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});
		InitializeServiceProvider();
		return builder.Build();
	}
	internal static void InitializeServiceProvider() {
		var serviceProvider = GetServiceProvider();
		ViewModelLocator.ServiceProvider = serviceProvider;
	}
	internal static ServiceProvider GetServiceProvider() {
		var serviceCollection = new ServiceCollection();
		InitializeServices(serviceCollection);
		return serviceCollection.BuildServiceProvider();
	}
	internal static void InitializeServices(ServiceCollection serviceCollection) {
		serviceCollection.UseViewModel();
	}
}
