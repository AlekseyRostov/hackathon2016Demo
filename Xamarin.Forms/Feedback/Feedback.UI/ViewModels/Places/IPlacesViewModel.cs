using System.Collections.ObjectModel;
using Feedback.Core.Entities;
using Feedback.UI.ViewModels.Base;

namespace Feedback.UI.ViewModels.Places
{
    public interface IPlacesViewModel : ILoadableViewModel
    {
        ObservableCollection<Place> Places { get; }
    }
}