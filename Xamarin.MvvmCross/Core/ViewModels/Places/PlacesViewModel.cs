using System.Collections.ObjectModel;
using Feedback.API.Entities;
using Feedback.Core.ViewModels.Commands;
using Feedback.Core.ViewModels.Places.Commands;
using PropertyChanged;

namespace Feedback.Core.ViewModels.Places
{
    [ImplementPropertyChanged]
    internal class PlacesViewModel : BaseLoadableViewModel, IPlacesViewModel
    {
        private IAsyncCommand _loadCommand;
        public override IAsyncCommand LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = new LoadPlacesCommand(this));
            }
        }
        
        private IAsyncCommand _logoutCommand;
        public IAsyncCommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new LogoutCommand(this));
            }
        }

        public ObservableCollection<Place> Places { get; set; }
    }
}