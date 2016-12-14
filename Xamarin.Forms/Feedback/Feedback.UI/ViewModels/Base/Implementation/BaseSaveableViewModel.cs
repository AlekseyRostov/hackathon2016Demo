using PropertyChanged;
using TheRX.MVVM.VisualSupport;

namespace Feedback.UI.ViewModels.Base.Implementation
{
    [ImplementPropertyChanged]
    public class BaseSaveableViewModel : ViewModel, ISaveableViewModel
    {
        public bool IsSaving { get; set; }
        public bool SaveSucceeded { get; set; }
        public IAsyncCommand SaveCommand { get; protected set; }
        public string SaveFailureMessage { get; set; }
    }
}