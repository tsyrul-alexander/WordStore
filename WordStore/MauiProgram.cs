using WordStore.Extension;
using WordStore.Handler;
using WordStore.View;
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
		InitializeServices(builder.Services);
		var app = builder.Build();
		InitializeViewModelLocator(app.Services);
		RegisterRoutes();
		return app;
	}
	internal static void InitializeViewModelLocator(IServiceProvider serviceProvider) {
		ViewModelLocator.ServiceProvider = serviceProvider;
	}
	internal static void InitializeServices(IServiceCollection serviceCollection) {
		serviceCollection.AddSingleton<AppSettings>();
		serviceCollection.AddSingleton<AppConstants>();
		serviceCollection.AddTransient<WordSearchHandler>();
		serviceCollection.UseViewModel();
		serviceCollection.UseFileManager();
		serviceCollection.UsePagination();
		serviceCollection.UseWordManager();
		serviceCollection.UseEFWordStorage();
		serviceCollection.UseDialogManager();
		serviceCollection.UseNavigatioManager();
	}
	internal static void RegisterRoutes() {
		Routing.RegisterRoute("word-details", typeof(WordDetailView));
	}
}
