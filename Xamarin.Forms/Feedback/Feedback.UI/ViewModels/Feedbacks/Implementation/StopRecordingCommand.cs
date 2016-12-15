using TheRX.MVVM.VisualSupport;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    internal class StopRecordingCommand : Command
    {
        private readonly FeedbackViewModel _viewModel;

        public StopRecordingCommand(FeedbackViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.RecordingPath = _viewModel.AudioRecorder.StopRecording();
            _viewModel.IsRecording = false;
        }
    }
}