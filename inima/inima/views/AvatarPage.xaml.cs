using inima.models;

namespace inima.views;

public partial class AvatarPage : ContentPage
{
	public AvatarPage(AvatarViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}