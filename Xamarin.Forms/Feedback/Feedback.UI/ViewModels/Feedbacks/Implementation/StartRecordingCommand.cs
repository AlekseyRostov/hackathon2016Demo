using TheRX.MVVM.VisualSupport;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    internal class StartRecordingCommand : Command
    {
        private readonly FeedbackViewModel _viewModel;

        public StartRecordingCommand(FeedbackViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.AudioRecorder.StartRecording();
            _viewModel.IsRecording = true;
        }
    }
}