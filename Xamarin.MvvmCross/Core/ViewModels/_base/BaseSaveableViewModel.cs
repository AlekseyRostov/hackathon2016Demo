using Feedback.Core.ViewModels.Commands;
using MvvmCross.Core.ViewModels;
using PropertyChanged;

namespace Feedback.Core.ViewModels
{
    [ImplementPropertyChanged]
    public abstract class BaseSaveableViewModel : MvxViewModel, ISaveableViewModel
    {
        public bool IsSaving { get; set; }

        public bool SaveSucceeded { get; set; }

        public abstract IAsyncCommand SaveCommand { get; }

        public string SaveFailureMessage { get; set; }
    }
}