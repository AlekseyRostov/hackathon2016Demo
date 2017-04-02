using System.ComponentModel;
using System.Windows.Input;
using Feedback.Core.Services;
using Feedback.Core.ViewModels.Commands;
using PropertyChanged;
using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;
using Feedback.Core.ViewModels.Feedbacks.Feedback.Commands;

namespace Feedback.Core.ViewModels.Feedbacks.Feedback
{
    [ImplementPropertyChanged]
    internal class FeedbackViewModel : BaseSaveableViewModel, IFeedbackViewModel
    {
        #region Commands

        private ICommand _startRecordingCommand;
        public ICommand StartRecordingCommand
        {
            get
            {
                return _startRecordingCommand ?? (_startRecordingCommand = new MvxCommand(() =>
                {
                    AudioRecorder.StartRecording();
                    IsRecording = true;
                }));
            }
        }

        private ICommand _stopRecordingCommand;
        public ICommand StopRecordingCommand
        {
            get
            {
                return _stopRecordingCommand ?? (_stopRecordingCommand = new MvxCommand(() =>
                {
                    RecordingPath = AudioRecorder.StopRecording();
                    IsRecording = false;
                }));
            }
        }

        private IAsyncCommand _speechToTextCommand;
        public IAsyncCommand SpeechToTextCommand
        {
            get
            {
                return _speechToTextCommand ?? (_speechToTextCommand = new SpeechToTextCommand(this));
            }
        }

        private IAsyncCommand _saveCommand;
        public override IAsyncCommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new SaveFeedbackCommand(this));
            }
        }

        #endregion

        #region Properties

        public string RecordingPath { get; internal set; }

        public bool IsRecording { get; internal set; }

        public string PlaceId { get; set; }

        public string Text { get; set; }

        public string UserEmail { get; set; }

        public bool IsRecognizingSpeech { get; internal set; }

        public string SpeechRecognitionError { get; internal set; }

        #endregion

        #region Services

        internal IAudioRecorderService AudioRecorder { get { return Mvx.Resolve<IAudioRecorderService>(); } }

        #endregion

        public FeedbackViewModel()
        {
            PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(RecordingPath))
            {
                SpeechToTextCommand.Execute(null);
            }
        }
    }
}