using System.Threading.Tasks;
using Feedback.Core.Services;

namespace Feedback.UI.ViewModels.Authentication.Implementation
{
    internal class LoginFacebookCommand : BaseLoginCommand
    {
        public LoginFacebookCommand(LoginViewModel viewModel,
                                    IAuthenticationService authenticationService) : base(viewModel, authenticationService)
        {
        }

        public override Task LoginAsync()
        {
            return AuthenticationService.LoginWithFacebookAsync();
        }
    }
}