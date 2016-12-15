using System.ComponentModel;

namespace Feedback.UI.ViewModels.Base
{
    public interface ISaveableViewModel : INotifyPropertyChanged
    {
        bool IsSaving { get; set; }
        bool SaveSucceeded { get; set; }
        IAsyncCommand SaveCommand { get; }
        string SaveFailureMessage { get; set; }
    }
}