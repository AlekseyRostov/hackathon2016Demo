using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Feedback.Core.Services;
using Feedback.UI.ViewModels.Base.Implementation;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    internal class LoadFeedbacksCommand : AsyncLoadCommand
    {
        private readonly IFeedbackService _feedbackService;
        private readonly FeedbacksViewModel _viewModel;

        public LoadFeedbacksCommand(FeedbacksViewModel viewModel,
                                    IFeedbackService feedbackService,
                                    IAuthenticationService authenticationService) : base(viewModel, authenticationService)
        {
            _viewModel = viewModel;
            _feedbackService = feedbackService;
        }

        protected override async Task ExecuteCoreAsync(object param)
        {
            if(string.IsNullOrEmpty(_viewModel.PlaceId)) throw new ArgumentException($"Please provide ${nameof(IFeedbacksViewModel.PlaceId)} for which you want to load feedbacks");
            var feedbacks = await _feedbackService.GetFeedbacksAsync(_viewModel.PlaceId);
            _viewModel.Feedbacks = feedbacks != null ? new ObservableCollection<Core.Entities.Feedback>(feedbacks) : null;
            _viewModel.IsEmpty = _viewModel?.Feedbacks?.Any() != true;
        }
    }
}