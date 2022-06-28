using DevExpress.Maui.Editors;

namespace jtcWhatsNearby.View;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void autoRadius_Unfocused(object sender, FocusEventArgs e)
	{
		((AutoCompleteEdit)sender).IsEnabled = false;
        ((AutoCompleteEdit)sender).IsEnabled = true;
	}

	private void autoType_Unfocused(object sender, FocusEventArgs e)
	{
		((AutoCompleteEdit)sender).IsEnabled = false;
		((AutoCompleteEdit)sender).IsEnabled = true;
	}
}