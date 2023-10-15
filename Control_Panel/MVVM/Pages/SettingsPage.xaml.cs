using Control_Panel.MVVM.ViewModels;

namespace Control_Panel.MVVM.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}