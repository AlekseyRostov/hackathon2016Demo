using Feedback.Core.Services;
using Feedback.UI.ViewModels.Base;
using Microsoft.Practices.Unity;

namespace Feedback.UI.ViewModels.Authentication.Implementation
{
    internal class AuthenticationFactory
    {
        private readonly IUnityContainer _container;

        public AuthenticationFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IAsyncCommand GetLoginFacebookCommand(LoginViewModel viewModel)
        {
            var authenticationService = _container.Resolve<IAuthenticationService>();
            return new LoginFacebookCommand(viewModel, authenticationService);
        }
    }
}