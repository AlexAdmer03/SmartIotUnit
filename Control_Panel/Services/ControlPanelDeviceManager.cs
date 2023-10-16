using Control_Panel.MVVM.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using SharedLibrary.Services;
using System.Diagnostics;

namespace Control_Panel.Services;

public class ControlPanelDeviceManager
{
    private readonly string _connectionString = "HostName=AlexA-IoTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=mqLguqPSuasiLIMHkxFmPX2Gl/jjB8A1tAIoTMTt8pE=";
    private readonly RegistryManager _registryManager;
    private readonly ServiceClient _serviceClient;
    private readonly System.Timers.Timer _timer;
    private readonly DeviceManager _deviceManager;

    public List<DeviceItem> Devices { get; private set; }
    public event Action DevicesUpdated;

    public ControlPanelDeviceManager(DeviceManager deviceManager)
    {

        _deviceManager = deviceManager;

        _registryManager = RegistryManager.CreateFromConnectionString(_connectionString);
        _serviceClient = ServiceClient.CreateFromConnectionString(_connectionString);

        Devices = new List<DeviceItem>();

        _timer = new System.Timers.Timer(5000);
        _timer.Elapsed += async (s, e) => await GetAllDevicesAsync();
        _timer.Start();
    }

    private async Task GetAllDevicesAsync()
    {
        try
        {
            var updated = false;
            var list = new List<Twin>();
            var result = _registryManager.CreateQuery("select * from devices");

            foreach (var item in await result.GetNextAsTwinAsync())
                list.Add(item);

            foreach (var device in list)
                if (!Devices.Any(x => x.DeviceId == device.DeviceId))
                {
                    var _device = new DeviceItem { DeviceId = device.DeviceId };

                    try { _device.DeviceType = device.Properties.Reported["deviceType"].ToString(); }
                    catch { }

                    try { _device.Vendor = device.Properties.Reported["vendor"].ToString(); }
                    catch { }

                    try { _device.Location = device.Properties.Reported["location"].ToString(); }
                    catch { }

                    try { _device.IsActive = bool.Parse(!string.IsNullOrEmpty(device.Properties.Reported["isActive"].ToString())); }
                    catch { }

                    Devices.Add(_device);
                    updated = true;
                }

            for (int i = Devices.Count - 1; i >= 0; i--)
            {
                if (!list.Any(x => x.DeviceId == Devices[i].DeviceId))
                {
                    Devices.RemoveAt(i);
                    updated = true;
                }
            }


            if (updated)
                DevicesUpdated.Invoke();

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        }
    }

    public async Task SendDirectMethodAsync(string deviceId, string methodName)
    {
        var methodInvocation = new CloudToDeviceMethod(methodName) { ResponseTimeout = TimeSpan.FromSeconds(30) };

        var response = await _serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);

        if (methodName.ToLower() == "start")
        {
            await UpdateDesiredPropertiesAsync(deviceId, "AllowSending", true);
        }
        else if (methodName.ToLower() == "stop")
        {
            await UpdateDesiredPropertiesAsync(deviceId, "AllowSending", false);
        }
        else
        {
            Debug.WriteLine($"Method invocation failed with status {response.Status}");
        }
    }

    public async Task UpdateDesiredPropertiesAsync(string deviceId, string propertyName, object newValue)
    {
        const int maxRetries = 3;
        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                var twin = await _registryManager.GetTwinAsync(deviceId);
                twin.Properties.Desired[propertyName] = newValue;
                await _registryManager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);

                break;
            }
            catch (Microsoft.Azure.Devices.Common.Exceptions.PreconditionFailedException ex)
            {
                Debug.WriteLine($"ETag mismatch on attempt {attempt}: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);

                if (attempt == maxRetries)
                {
                    throw;
                }
            }
        }
    }
}
