using Feedback.Core.Services;

namespace Feedback.Droid.Services
{
    public class DeviceService : IDeviceService
    {
        public string OperatingSystem => "Android";
        public int AudioSampleRate { get; set; } = 44100;
    }
}