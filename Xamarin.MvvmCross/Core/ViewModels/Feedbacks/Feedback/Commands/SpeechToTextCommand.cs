using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Feedback.Core.Services;
using Feedback.Core.ViewModels.Commands;
using PCLStorage;
using Strings = Feedback.Core.Resources.Strings.Feedbacks.Common;
using MvvmCross.Platform;

namespace Feedback.Core.ViewModels.Feedbacks.Feedback.Commands
{
    internal class SpeechToTextCommand : AsyncCommand
    {
        private readonly FeedbackViewModel _viewModel;

        protected ISpeechService SpeechService { get { return Mvx.Resolve<ISpeechService>(); } }

        public SpeechToTextCommand(FeedbackViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsRecognizingSpeech = true;
            try
            {
                var text = await SpeechService.SpeechToTextAsync(_viewModel.RecordingPath, "en-US");

                _viewModel.Text += " " + text;

                await DeleteRecording(_viewModel.RecordingPath).ConfigureAwait(false);
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

        private async Task DeleteRecording(string recordingPath)
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