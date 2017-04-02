using System.Threading.Tasks;
using Feedback.API.Services;
using Feedback.Core.ViewModels.Commands;
using MvvmCross.Platform;

namespace Feedback.Core.ViewModels.Feedbacks.Feedback.Commands
{
    internal class SaveFeedbackCommand : AsyncSaveCommand<IFeedbackViewModel>
    {
        protected IFeedbackService FeedbackService { get { return Mvx.Resolve<IFeedbackService>(); } }

        public SaveFeedbackCommand(IFeedbackViewModel viewModel) 
            : base(viewModel)
        {
        }

        protected override async Task ExecuteCoreAsync(object param)
        {
            await FeedbackService.SaveFeedbackAsync(ViewModel.PlaceId, ViewModel.UserEmail, ViewModel.Text);
        }
    }
}