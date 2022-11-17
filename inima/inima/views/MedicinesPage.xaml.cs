using inima.models;

namespace inima.views;

public partial class MedicinesPage : ContentPage
{
	public MedicinesPage(MedicinesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}