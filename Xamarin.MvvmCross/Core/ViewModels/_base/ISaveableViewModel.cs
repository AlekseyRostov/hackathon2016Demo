using Feedback.Core.ViewModels.Commands;
using MvvmCross.Core.ViewModels;

namespace Feedback.Core.ViewModels
{
    public interface ISaveableViewModel : IMvxViewModel
    {
        bool IsSaving { get; set; }

        bool SaveSucceeded { get; set; }

        IAsyncCommand SaveCommand { get; }

        string SaveFailureMessage { get; set; }
    }
}