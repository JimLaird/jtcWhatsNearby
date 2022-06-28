namespace jtcWhatsNearby;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MapDisplayPage), typeof(MapDisplayPage));
		Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
	}
}
