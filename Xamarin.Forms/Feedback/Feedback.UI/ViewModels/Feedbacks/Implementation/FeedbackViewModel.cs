using Feedback.UI.ViewModels.Base;
using Feedback.UI.ViewModels.Base.Implementation;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    internal class FeedbackViewModel : BaseSaveableViewModel, IFeedbackViewModel
    {
        public FeedbackViewModel(FeedbacksFactory factory)
        {
            SaveCommand = factory.GetSaveFeedbackCommand(this);
        }

        public string PlaceId { get; set; }
        public string Text { get; set; }
        public string UserEmail { get; set; }
    }
}