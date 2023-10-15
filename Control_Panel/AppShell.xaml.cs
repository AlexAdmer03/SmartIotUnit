using Control_Panel.MVVM.Pages;

namespace Control_Panel
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DeviceMenuPage), typeof(DeviceMenuPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(AllDevicesPage), typeof(AllDevicesPage));
        }
    }
}