using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Feedback.Core.Entities;
using Feedback.Core.Services;
using Feedback.UI.ViewModels.Base.Implementation;

namespace Feedback.UI.ViewModels.Places.Implementation
{
    internal class LoadPlacesCommand : AsyncLoadCommand
    {
        private readonly PlacesViewModel _viewModel;
        private readonly IPlaceService _placesService;

        public LoadPlacesCommand(PlacesViewModel viewModel, IPlaceService placesService) : base(viewModel)
        {
            _placesService = placesService;
            _viewModel = viewModel;
        }

        protected override async Task ExecuteCoreAsync(object param)
        {
            var places = await _placesService.GetPlacesAsync();
            _viewModel.Places = new ObservableCollection<Place>(places);
            _viewModel.IsEmpty = _viewModel.Places?.Any() != true;
        }
    }
}