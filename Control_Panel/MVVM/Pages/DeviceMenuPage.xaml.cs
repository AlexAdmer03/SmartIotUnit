using Control_Panel.MVVM.ViewModels;

namespace Control_Panel.MVVM.Pages;

public partial class DeviceMenuPage : ContentPage
{
	private DeviceMenuViewModel _viewModel;
	public DeviceMenuPage(DeviceMenuViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
		_viewModel = viewModel;
	}

    private void DeviceSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        _viewModel.ToggleFanState(e);
    }
}