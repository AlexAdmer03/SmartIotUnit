using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Control_Panel.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Control_Panel.MVVM.ViewModels
{
    public partial class AllDevicesViewModel : ObservableObject
    {
        private readonly ControlPanelDeviceManager _controlPanelDeviceManager;
        //private readonly SmartHomeDbContext _context;

        [ObservableProperty]
        bool isConfigured;

        [ObservableProperty]
        ObservableCollection<DeviceItemViewModel> devices;

        [RelayCommand]
        public async Task SendDirectMethod(DeviceItemViewModel deviceItem)
        {
            try
            {
                var methodName = string.Empty;

                if (!deviceItem.IsActive)
                {
                    deviceItem.IsActive = true;
                    methodName = "start";
                }
                else
                {
                    deviceItem.IsActive = false;
                    methodName = "stop";
                }

                await _controlPanelDeviceManager.SendDirectMethodAsync(deviceItem.DeviceId, methodName);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); deviceItem.IsActive = false; }
        }



        public AllDevicesViewModel(ControlPanelDeviceManager deviceManager/* SmartHomeDbContext context*/)
        {
            _controlPanelDeviceManager = deviceManager;
            //_context = context;

            IsConfigured = false;
            IsConfigured = true;

            if (!IsConfigured)
            {
                IsConfigured = true;
            }



            Devices = new ObservableCollection<DeviceItemViewModel>(_controlPanelDeviceManager.Devices
                .Select(device => new DeviceItemViewModel(device)).ToList());

            _controlPanelDeviceManager.DevicesUpdated += UpdateDeviceList;



        }

        private void UpdateDeviceList()
        {
            Devices = new ObservableCollection<DeviceItemViewModel>(_controlPanelDeviceManager.Devices
                .Select(device => new DeviceItemViewModel(device)).ToList());
        }

        //private async Task AddConnectionStringAsync(string connectionString)
        //{
        //    _context.Settings.Add(new SharedSmartLibrary.Entities.SmartAppSettings { ConnectionString = connectionString });
        //    await _context.SaveChangesAsync();
        //}

        //private async Task<string> GetConnectionStringAsync()
        //{
        //    var result = await _context.Settings.FirstOrDefaultAsync();
        //    if (result != null)
        //        return result.ConnectionString;

        //    return null!;
        //}

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
