using System.Collections.ObjectModel;
using Feedback.UI.ViewModels.Base;

namespace Feedback.UI.ViewModels.Feedbacks
{
    public interface IFeedbacksViewModel : ILoadableViewModel
    {
        string PlaceId { get; set; }
        ObservableCollection<Core.Entities.Feedback> Feedbacks { get; }
    }
}