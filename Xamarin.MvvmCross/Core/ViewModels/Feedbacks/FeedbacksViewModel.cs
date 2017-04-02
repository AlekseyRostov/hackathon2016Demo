using System.Collections.ObjectModel;
using Feedback.Core.ViewModels;
using Feedback.Core.ViewModels.Commands;
using Feedback.Core.ViewModels.Feedbacks;
using Feedback.Core.ViewModels.Feedbacks.Commands;

namespace Feedback.UI.ViewModels.Feedbacks.Implementation
{
    internal class FeedbacksViewModel : BaseLoadableViewModel, IFeedbacksViewModel
    {
        private IAsyncCommand _loadCommand;
        public override IAsyncCommand LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = new LoadFeedbacksCommand(this));
            }
        }

        public string PlaceId { get; set; }

        public string PlaceName { get; set; }

        public ObservableCollection<API.Entities.Feedback> Feedbacks { get; set; }
    }
}