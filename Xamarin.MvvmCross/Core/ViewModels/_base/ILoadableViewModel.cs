using Feedback.Core.ViewModels.Commands;
using MvvmCross.Core.ViewModels;

namespace Feedback.Core.ViewModels
{
    public interface ILoadableViewModel : IMvxViewModel
    {
        bool IsLoading { get; set; }

        string LoadFailureMessage { get; set; }

        IAsyncCommand LoadCommand { get; }

        bool IsLoaded { get; set; }

        bool IsEmpty { get; set; }
    }
}