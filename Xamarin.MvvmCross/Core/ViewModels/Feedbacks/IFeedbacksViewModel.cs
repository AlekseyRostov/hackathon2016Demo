using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Feedback.Core.ViewModels.Feedbacks
{
    public interface IFeedbacksViewModel : ILoadableViewModel
    {
        ICommand AddCommand { get; }

        string PlaceId { get; }

        string PlaceName { get; }

        ObservableCollection<API.Entities.Feedback> Feedbacks { get; set; }
    }
}