using Feedback.Core.Services;

namespace Feedback.iOS.Services
{
    public class DeviceService : IDeviceService
    {
        public string OperatingSystem => "iPhone OS";
        public int AudioSampleRate { get; set; } = 44100;
    }
}