using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Feedback.API.Services;
using Feedback.Core.ViewModels.Commands;
using MvvmCross.Platform;

namespace Feedback.Core.ViewModels.Feedbacks.Commands
{
    internal class LoadFeedbacksCommand : AsyncLoadCommand<IFeedbacksViewModel>
    {
        protected IFeedbackService FeedbackService { get { return Mvx.Resolve<IFeedbackService>(); } }

        public LoadFeedbacksCommand(IFeedbacksViewModel viewModel) 
            : base(viewModel)
        {
        }

        protected override async Task ExecuteCoreAsync(object param)
        {
            if (string.IsNullOrEmpty(ViewModel.PlaceId)) 
                throw new ArgumentException($"Please provide ${nameof(IFeedbacksViewModel.PlaceId)} for which you want to load feedbacks");
            
            var feedbacks = await FeedbackService.GetFeedbacksAsync(ViewModel.PlaceId);
            ViewModel.Feedbacks = feedbacks != null ? new ObservableCollection<API.Entities.Feedback>(feedbacks) : null;
            ViewModel.IsEmpty = ViewModel?.Feedbacks?.Any() != true;
        }
    }
}
