using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using WordStore.Extension;
using WordStore.Handler;
using WordStore.View;
using WordStore.ViewModel;

namespace WordStore;

public static class MauiProgram {
	public static MauiApp CreateMauiApp() {
		var builder = MauiApp.CreateBuilder()
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts => {
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});
		InitializeServices(builder.Services);
		InitializeConfiguration(builder.Configuration);
		var app = builder.Build();
		InitializeViewModelLocator(app.Services);
		RegisterRoutes();
		return app;
	}
	internal static void InitializeViewModelLocator(IServiceProvider serviceProvider) {
		ViewModelLocator.ServiceProvider = serviceProvider;
	}
	internal static void InitializeConfiguration(ConfigurationManager configurationManager) {
		configurationManager.AddJsonFile("appsettings.json");
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
		serviceCollection.UseBookReader();
	}
	internal static void RegisterRoutes() {
		Routing.RegisterRoute("word-details", typeof(WordDetailView));
	}
}
