using CommunityToolkit.Maui;
using Control_Panel.MVVM.Pages;
using Control_Panel.MVVM.ViewModels;
using Microsoft.Extensions.Logging;

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

                });

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<DeviceMenuViewModel>();
            builder.Services.AddSingleton<DeviceMenuPage>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<AllDevicesPage>();
            builder.Services.AddSingleton<AllDevicesViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}