using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Control_Panel.MVVM.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_Panel.MVVM.ViewModels
{
    public partial class DeviceMenuViewModel : ObservableObject
    {
        [RelayCommand]
        async Task GoToSettings() => await Shell.Current.GoToAsync(nameof(SettingsPage));

        [RelayCommand]
        async Task GoToAllDevices() => await Shell.Current.GoToAsync(nameof(AllDevicesPage));
    }
}
