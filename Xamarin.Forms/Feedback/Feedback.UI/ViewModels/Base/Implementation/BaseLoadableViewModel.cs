using PropertyChanged;
using TheRX.MVVM.VisualSupport;

namespace Feedback.UI.ViewModels.Base.Implementation
{
    [ImplementPropertyChanged]
    internal abstract class BaseLoadableViewModel : ViewModel, ILoadableViewModel
    {
        public IAsyncCommand LoadCommand { get; protected set; }

        public bool IsLoading { get; set; }

        public string LoadFailureMessage { get; set; }

        public bool IsLoaded { get; set; }

        public bool IsEmpty { get; set; }
    }
}