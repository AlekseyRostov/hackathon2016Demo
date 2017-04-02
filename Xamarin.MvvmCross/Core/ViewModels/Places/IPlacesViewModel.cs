using System.Collections.ObjectModel;
using Feedback.API.Entities;

namespace Feedback.Core.ViewModels.Places
{
    public interface IPlacesViewModel : ILoadableViewModel
    {
        ObservableCollection<Place> Places { get; set; }
    }
}