namespace jtcWhatsNearby.View;

public partial class MapDisplayPage : ContentPage
{
	public MapDisplayPage(MapDisplayViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}