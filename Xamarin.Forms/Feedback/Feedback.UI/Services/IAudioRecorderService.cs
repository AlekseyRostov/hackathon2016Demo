namespace Feedback.UI.Services
{
    public interface IAudioRecorderService
    {
        void StartRecording();
        string StopRecording();
    }
}