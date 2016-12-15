using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Feedback.Core.Services.Cognitive;
using Feedback.UI.ViewModels.Base.Implementation;
using PCLStorage;
using Strings = Feedback.UI.Resources.Strings.Feedbacks.Common;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    internal class SpeechToTextCommand : AsyncCommand
    {
        private readonly FeedbackViewModel _viewModel;
        private readonly ISpeechService _speechService;

        public SpeechToTextCommand(FeedbackViewModel viewModel, ISpeechService speechService)
        {
            _viewModel = viewModel;
            _speechService = speechService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsRecognizingSpeech = true;
            try
            {
                var text = await _speechService.SpeechToTextAsync(_viewModel.RecordingPath, "en-US");
                _viewModel.Text += " " + text;
                DeleteRecording(_viewModel.RecordingPath);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                _viewModel.SpeechRecognitionError = Strings.SpeechRecognitionFailure;
            }
            finally
            {
                _viewModel.IsRecognizingSpeech = false;
            }
        }

        private async void DeleteRecording(string recordingPath)
        {
            try
            {
                var file = await FileSystem.Current.GetFileFromPathAsync(recordingPath);
                await file.DeleteAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}