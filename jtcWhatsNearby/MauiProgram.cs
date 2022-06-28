using CommunityToolkit.Maui;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using DevExpress.Maui;
using DevExpress.Maui.Editors;




namespace jtcWhatsNearby;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseDevExpress()
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler<AutoCompleteEdit, AutoCompleteEditHandler>();
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
				fonts.AddFont("Mulish-Black.ttf", "MulishBlack");
				fonts.AddFont("Mulish-Regular.ttf", "MulishRegular");
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//	Add Services
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);
		builder.Services.AddSingleton<IPhoneDialer>(PhoneDialer.Default);
		builder.Services.AddSingleton<IBrowser>(Browser.Default);
		builder.Services.AddSingleton<PlaceService>();

		//	Add ViewModels
		builder.Services.AddSingleton<SearchPageViewModel>();
		builder.Services.AddTransient<DetailsViewModel>();
		builder.Services.AddTransient<MapDisplayViewModel>();

		//	Add Views
		builder.Services.AddSingleton<SearchPage>();
		builder.Services.AddTransient<DetailsPage>();
		builder.Services.AddTransient<MapDisplayPage>();

		return builder.Build();
	}
}
