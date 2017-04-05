using System.Collections.ObjectModel;
using Feedback.API.Entities;
using System.Windows.Input;
using Feedback.Core.ViewModels.Commands;

namespace Feedback.Core.ViewModels.Places
{
    public interface IPlacesViewModel : ILoadableViewModel
    {
        ICommand SelectionChangedCommand { get; }

        IAsyncCommand LogoutCommand { get; }

        ObservableCollection<Place> Places { get; set; }
    }
}