using Feedback.Core.ViewModels.Commands;
using MvvmCross.Core.ViewModels;
using PropertyChanged;

namespace Feedback.Core.ViewModels
{
    [ImplementPropertyChanged]
    internal abstract class BaseLoadableViewModel : MvxViewModel, ILoadableViewModel
    {
        public abstract IAsyncCommand LoadCommand { get; }

        public bool IsLoading { get; set; }

        public string LoadFailureMessage { get; set; }

        public bool IsLoaded { get; set; }

        public bool IsEmpty { get; set; }
    }
}