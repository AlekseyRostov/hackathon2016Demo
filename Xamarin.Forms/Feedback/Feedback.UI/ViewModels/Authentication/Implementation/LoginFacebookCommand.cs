using System.Threading.Tasks;
using Feedback.Core.Entities;
using Feedback.Core.Services;

namespace Feedback.UI.ViewModels.Authentication.Implementation
{
    internal class LoginFacebookCommand : BaseLoginCommand
    {
        public LoginFacebookCommand(LoginViewModel viewModel,
                                    IAuthenticationService authenticationService) : base(viewModel, authenticationService)
        {
        }

        public override async Task<User> LoginAsync()
        {
            var user = await AuthenticationService.LoginWithFacebookAsync();
            return user;
        }
    }
}