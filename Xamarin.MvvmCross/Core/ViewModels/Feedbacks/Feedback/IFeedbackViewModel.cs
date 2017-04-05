using System.Windows.Input;
using Feedback.Core.ViewModels.Commands;

namespace Feedback.Core.ViewModels.Feedbacks.Feedback
{
    public interface IFeedbackViewModel : ISaveableViewModel
    {
        string PlaceId { get; }
        string PlaceName { get; }

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