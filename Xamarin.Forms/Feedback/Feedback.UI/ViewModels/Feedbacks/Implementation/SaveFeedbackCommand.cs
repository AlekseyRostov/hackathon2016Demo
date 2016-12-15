using System.Threading.Tasks;
using Feedback.Core.Services;
using Feedback.UI.ViewModels.Base.Implementation;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    internal class SaveFeedbackCommand : AsyncSaveCommand
    {
        private readonly IFeedbackService _feedbackService;
        private readonly FeedbackViewModel _viewModel;

        public SaveFeedbackCommand(FeedbackViewModel viewModel,
                                   IFeedbackService feedbackService,
                                   IAuthenticationService authenticationService) : base(viewModel, authenticationService)
        {
            _viewModel = viewModel;
            _feedbackService = feedbackService;
        }

        protected override async Task ExecuteCoreAsync(object param)
        {
            await _feedbackService.SaveFeedbackAsync(_viewModel.PlaceId, _viewModel.UserEmail, _viewModel.Text);
        }
    }
}