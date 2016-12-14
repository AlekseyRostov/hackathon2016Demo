using System.ComponentModel;
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
            SpeechToTextCommand = factory.GetSpeechToTextCommand(this);
            PropertyChanged += OnViewModelPropertyChanged;
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
        public bool IsRecognizingSpeech { get; internal set; }
        public string SpeechRecognitionError { get; internal set; }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(RecordingPath))
            {
                SpeechToTextCommand.Execute(null);
            }
        }
    }
}