using System.Windows.Input;
using Feedback.UI.ViewModels.Base;

namespace Feedback.UI.ViewModels.Feedbacks
{
    public interface IFeedbackViewModel : ISaveableViewModel
    {
        string PlaceId { get; set; }
        string Text { get; set; }
        string UserEmail { get; set; }

        ICommand StartRecordingCommand { get; }
        ICommand StopRecordingCommand { get; }
        IAsyncCommand SpeechToTextCommand { get; }
        string RecordingPath { get; }
        bool IsRecording { get; }
        bool IsRecognizingSpeech { get; }
        string SpeechRecognitionError { get; }
    }
}