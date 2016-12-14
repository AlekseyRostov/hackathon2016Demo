using System.Collections.ObjectModel;
using Feedback.UI.ViewModels.Base.Implementation;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    internal class FeedbacksViewModel : BaseLoadableViewModel, IFeedbacksViewModel
    {
        public FeedbacksViewModel(FeedbacksFactory factory)
        {
            LoadCommand = factory.GetLoadFeedbacksCommand(this);
        }

        public string PlaceId { get; set; }
        public string PlaceName { get; set; }
        public ObservableCollection<Core.Entities.Feedback> Feedbacks { get; internal set; }
    }
}