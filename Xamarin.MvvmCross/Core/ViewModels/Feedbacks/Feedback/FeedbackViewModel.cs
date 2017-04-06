using System.ComponentModel;
using System.Windows.Input;
using Feedback.Core.Services;
using Feedback.Core.ViewModels.Commands;
using PropertyChanged;
using MvvmCross.Platform;
using MvvmCross.Core.ViewModels;
using Feedback.Core.ViewModels.Feedbacks.Feedback.Commands;
using Feedback.Core.Messages;
using MvvmCross.Plugins.Messenger;

namespace Feedback.Core.ViewModels.Feedbacks.Feedback
{
    [ImplementPropertyChanged]
    internal class FeedbackViewModel : BaseSaveableViewModel, IFeedbackViewModel
    {
        #region Fields

        private MvxSubscriptionToken _subscriptionToken;

        #endregion

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

                    RaisePropertyChanged(() => IsRecording);
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

        public string PlaceName { get; set; }

        public string Text { get; set; }

        public string UserEmail { get; set; }

        public bool IsRecognizingSpeech { get; internal set; }

        public string SpeechRecognitionError { get; internal set; }

        #endregion

        #region Services

        private IAudioRecorderService _audioRecorder;
        internal IAudioRecorderService AudioRecorder
        {
            get
            {
                return _audioRecorder ?? (_audioRecorder = Mvx.Resolve<IAudioRecorderService>()); 
            }
        }

        #endregion

        public FeedbackViewModel()
        {
            PropertyChanged += OnViewModelPropertyChanged;

            ShouldAlwaysRaiseInpcOnUserInterfaceThread(true);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(RecordingPath))
            {
                SpeechToTextCommand.Execute(null);
            }
            else if (e.PropertyName == nameof(SaveSucceeded) || e.PropertyName == nameof(SaveFailureMessage))
            {
                if (SaveSucceeded)
                {
                    Mvx.Resolve<IMvxMessenger>().Publish(new FeedbackSavedMessage(this));
                    Close(this); 
                }
                else if (!string.IsNullOrEmpty(SaveFailureMessage))
                {
                    
                }
            }
        }

        private void OnBeaconFound(BeaconFoundMessage msg)
        {
            PlaceId = msg.Id;
            PlaceName = msg.Name;
        }

        public void Init(string id, string name)
        {
            PlaceId = id;
            PlaceName = name;

            UserEmail = "test@mail.com";

            _subscriptionToken = Mvx.Resolve<IMvxMessenger>().Subscribe<BeaconFoundMessage>(OnBeaconFound);
        }
    }
}