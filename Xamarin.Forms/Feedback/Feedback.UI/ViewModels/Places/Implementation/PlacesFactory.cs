using System.Threading.Tasks;
using Feedback.Core.Services;
using Feedback.UI.ViewModels.Base;
using Feedback.UI.ViewModels.Base.Implementation;
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

        public IAsyncCommand GetLogoutCommand(PlacesViewModel viewModel)
        {
            var authenticationService = _container.Resolve<IAuthenticationService>();
            return new LogoutCommand(viewModel, authenticationService);
        }
    }

    internal class LogoutCommand : AsyncCommand
    {
        private readonly IAuthenticationService _authenticationService;

        public LogoutCommand(PlacesViewModel viewModel, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public override Task ExecuteAsync(object parameter)
        {
            return _authenticationService.LogoutAsync();
        }
    }
}