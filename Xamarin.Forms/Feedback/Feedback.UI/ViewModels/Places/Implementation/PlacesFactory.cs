using Feedback.Core.Services;
using Feedback.UI.ViewModels.Base;
using Microsoft.Practices.Unity;

namespace Feedback.UI.ViewModels.Places.Implementation
{
    internal class PlacesFactory
    {
        private readonly IUnityContainer _container;

        public PlacesFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IAsyncCommand GetLoadPlacesCommand(PlacesViewModel viewModel)
        {
            var placesService = _container.Resolve<IPlaceService>();
            var authenticationService = _container.Resolve<IAuthenticationService>();
            return new LoadPlacesCommand(viewModel, placesService, authenticationService);
        }
    }
}