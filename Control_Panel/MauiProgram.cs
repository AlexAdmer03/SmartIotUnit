using CommunityToolkit.Maui;
using Control_Panel.MVVM.Pages;
using Control_Panel.MVVM.ViewModels;
using Control_Panel.Services;
using Microsoft.Extensions.Logging;
using SharedLibrary.Services;

namespace Control_Panel
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                    fonts.AddFont("Rubik-Regular.ttf", "RubikRegular");

                });

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<DeviceMenuViewModel>();
            builder.Services.AddSingleton<DeviceMenuPage>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<AllDevicesPage>();
            builder.Services.AddSingleton<AllDevicesViewModel>();
            builder.Services.AddSingleton<DeviceManager>();
            builder.Services.AddSingleton<ControlPanelDeviceManager>();
            builder.Services.AddSingleton<IotHubService>();
            builder.Services.AddSingleton<DeviceItemViewModel>();

            builder.Services.AddSingleton<DateAndTimeService>();
            builder.Services.AddSingleton<WeatherService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}