using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Feedback.API.Entities;
using Feedback.API.Services;
using Feedback.Core.ViewModels.Commands;
using MvvmCross.Platform;

namespace Feedback.Core.ViewModels.Places.Commands
{
    internal class LoadPlacesCommand : AsyncLoadCommand<IPlacesViewModel>
    {
        protected IPlaceService PlacesService { get { return Mvx.Resolve<IPlaceService>(); } }

        public LoadPlacesCommand(IPlacesViewModel viewModel)
            : base (viewModel)
        {

        }

        protected override async Task ExecuteCoreAsync(object param)
        {
            var places = await PlacesService.GetPlacesAsync();
            ViewModel.Places = places != null ? new ObservableCollection<Place>(places) : null;
            ViewModel.IsEmpty = ViewModel.Places?.Any() != true;
        }
    }
}