namespace Feedback.Core.Services
{
    public interface IAudioRecorderService
    {
        void StartRecording();
        string StopRecording();
    }
}