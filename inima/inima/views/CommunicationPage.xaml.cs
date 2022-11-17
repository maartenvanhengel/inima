using inima.models;

namespace inima.views;

public partial class CommunicationPage : ContentPage
{
	public CommunicationPage(CommunicationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

	private void ImageButton_Clicked(object sender, EventArgs e)
	{
		entry.IsEnabled = false;
		entry.IsEnabled = true;
	}
}