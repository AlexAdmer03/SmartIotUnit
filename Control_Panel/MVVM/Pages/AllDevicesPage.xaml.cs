using Control_Panel.MVVM.ViewModels;

namespace Control_Panel.MVVM.Pages;

public partial class AllDevicesPage : ContentPage
{
	public AllDevicesPage(AllDevicesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}