using Control_Panel.MVVM.ViewModels;

namespace Control_Panel.MVVM.Pages;

public partial class DeviceMenuPage : ContentPage
{
	public DeviceMenuPage(DeviceMenuViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
	}

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    var viewModel = BindingContext as MainViewModel;
    //    await viewModel?.CheckConfiguration();
    //}


    //private void Switch_Toggled(object sender, ToggledEventArgs e)
    //{
    //    var _switch = (Switch)sender;
    //    var iotDevice = _switch.BindingContext as IotDevice;

    //    var viewModel = BindingContext as MainViewModel;
    //    if (iotDevice == null)
    //    {
    //        viewModel.ToggleStateCommand.Execute(iotDevice);
    //    }
    //}
}