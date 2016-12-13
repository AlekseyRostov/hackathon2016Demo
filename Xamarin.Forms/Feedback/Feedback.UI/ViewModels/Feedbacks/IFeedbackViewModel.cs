using Feedback.UI.ViewModels.Base;

namespace Feedback.UI.ViewModels.Feedbacks
{
    public interface IFeedbackViewModel : ISaveableViewModel
    {
        string PlaceId { get; set; }
        string Text { get; set; }
        string UserEmail { get; set; }
    }
}