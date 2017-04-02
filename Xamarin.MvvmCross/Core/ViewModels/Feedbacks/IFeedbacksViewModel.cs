using System.Collections.ObjectModel;

namespace Feedback.Core.ViewModels.Feedbacks
{
    public interface IFeedbacksViewModel : ILoadableViewModel
    {
        string PlaceId { get; set; }

        string PlaceName { get; set; }

        ObservableCollection<API.Entities.Feedback> Feedbacks { get; set; }
    }
}