using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Control_Panel.MVVM.Pages;
using Control_Panel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Control_Panel.MVVM.ViewModels
{
    public partial class DeviceMenuViewModel : ObservableObject
    {
        private readonly ControlPanelDeviceManager _controlPanelDeviceManager;
        private readonly DateAndTimeService _dateAndTimeService;
        private readonly WeatherService _weatherService;
        public DeviceMenuViewModel(ControlPanelDeviceManager controlPanelDeviceManager, DateAndTimeService dateAndTimeService,
            WeatherService weatherService) 
        {
            _controlPanelDeviceManager = controlPanelDeviceManager;
            _dateAndTimeService = dateAndTimeService;
            _weatherService = weatherService;

            UpdateDateAndTime();
            UpdateWeather();
        }

        [ObservableProperty]
        private string _currentTime = "--:--";
        [ObservableProperty]
        private string _currentDate;
        [ObservableProperty]
        private string _currentWeatherCondition = "\uf185";
        [ObservableProperty]
        private string _currentTemperature = "--";
        [ObservableProperty]
        private string _currentTemperatureUnit = "°C";

        [RelayCommand]
        async Task GoToSettings() => await Shell.Current.GoToAsync(nameof(SettingsPage));

        [RelayCommand]
        async Task GoToAllDevices() => await Shell.Current.GoToAsync(nameof(AllDevicesPage));

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        private string _fanConnectionStatusText;

        public string FanConnectionStatusText
        {
            get => _fanConnectionStatusText;
            set => SetProperty(ref _fanConnectionStatusText, value);
        }

        private bool _isFanConnectionStatusVisible;
        public bool IsFanConnectionStatusVisible
        {
            get => _isFanConnectionStatusVisible;
            set => SetProperty(ref _isFanConnectionStatusVisible, value);
        }

        private bool _isFanConnected;
        public bool IsFanConnected
        {
            get => _isFanConnected;
            set => SetProperty(ref _isFanConnected, value);
        }

        public ICommand ToggleFanStateCommand { get; private set; }

        public async void ToggleFanState(ToggledEventArgs e)
        {
            bool isToggled = e.Value;
            var deviceId = "fan_device";
            string methodName = isToggled ? "start" : "stop";
            try
            {
                await _controlPanelDeviceManager.SendDirectMethodAsync(deviceId, methodName);
                IsFanConnected = isToggled;
            }
            catch (Microsoft.Azure.Devices.Common.Exceptions.DeviceNotFoundException)
            {
                IsFanConnected = false;
                FanConnectionStatusText = "Device Not Connected";
                IsFanConnectionStatusVisible = true;

                await Task.Delay(3000);

                IsFanConnectionStatusVisible = false;
            }
        }

        private void UpdateDateAndTime()
        {
            _dateAndTimeService.TimeUpdated += () =>
            {
                CurrentDate = _dateAndTimeService.CurrentDate;
                CurrentTime = _dateAndTimeService.CurrentTime;
            };
        }
        private void UpdateWeather()
        {
            _dateAndTimeService.TimeUpdated += () =>
            {
                CurrentWeatherCondition = _weatherService.CurrentWeatherCondition;
                CurrentTemperature = _weatherService.CurrentTemperature;
            };
        }


    }
}
