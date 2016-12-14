using System.Windows.Input;
using Feedback.UI.Services;
using Feedback.UI.ViewModels.Base;
using Feedback.UI.ViewModels.Base.Implementation;
using PropertyChanged;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    [ImplementPropertyChanged]
    internal class FeedbackViewModel : BaseSaveableViewModel, IFeedbackViewModel
    {
        public FeedbackViewModel(FeedbacksFactory factory, IAudioRecorderService audioRecorder)
        {
            SaveCommand = factory.GetSaveFeedbackCommand(this);
            AudioRecorder = audioRecorder;
            StartRecordingCommand = new StartRecordingCommand(this);
            StopRecordingCommand = new StopRecordingCommand(this);
        }

        public ICommand StartRecordingCommand { get; }
        public ICommand StopRecordingCommand { get; }
        public IAsyncCommand SpeechToTextCommand { get; }
        internal IAudioRecorderService AudioRecorder { get; }
        public string RecordingPath { get; internal set; }
        public bool IsRecording { get; internal set; }
        public string PlaceId { get; set; }
        public string Text { get; set; }
        public string UserEmail { get; set; }
    }
}