using System.Threading.Tasks;

namespace Feedback.Core.ViewModels.Login.Commands
{
    internal class LoginFacebookCommand : BaseLoginCommand
    {
        public LoginFacebookCommand(ILoginViewModel viewModel) 
            : base(viewModel)
        {
        }

        public override Task LoginAsync()
        {
            return AuthenticationService.LoginWithFacebookAsync();
        }
    }
}