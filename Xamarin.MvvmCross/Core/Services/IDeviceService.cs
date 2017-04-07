namespace Feedback.Core.Services
{
    public interface IDeviceService
    {
        string OperatingSystem { get; }

        int AudioSampleRate { get; set; }
    }
}