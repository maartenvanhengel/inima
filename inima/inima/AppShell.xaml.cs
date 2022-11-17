using inima.views;

namespace inima;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
		Routing.RegisterRoute(nameof(MedicinesPage), typeof(MedicinesPage));
		Routing.RegisterRoute(nameof(AvatarPage), typeof(AvatarPage));
		Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        Routing.RegisterRoute(nameof(CommunicationPage), typeof(CommunicationPage));
    }
}
