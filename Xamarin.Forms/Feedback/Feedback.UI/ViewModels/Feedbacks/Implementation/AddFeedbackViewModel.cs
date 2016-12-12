using Feedback.UI.ViewModels.Base;
using Feedback.UI.ViewModels.Base.Implementation;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    internal class AddFeedbackViewModel : BaseLoadableViewModel, IAddFeedbackViewModel
    {
        public AddFeedbackViewModel(FeedbacksFactory factory)
        {
            SaveCommand = factory.GetSaveFeedbackCommand(this);
        }

        public IAsyncCommand SaveCommand { get; }
        public string PlaceId { get; set; }
        public string Text { get; set; }
        public string UserEmail { get; set; }
    }
}