using inima.models;
using inima.views;

namespace inima;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddTransient<MedicinesPage>();
        builder.Services.AddTransient<MedicinesViewModel>();

        builder.Services.AddTransient<AvatarPage>();
        builder.Services.AddTransient<AvatarViewModel>();

        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsViewModel>();

        builder.Services.AddTransient<CommunicationPage>();
        builder.Services.AddTransient<CommunicationViewModel>();

        return builder.Build();
	}
}
