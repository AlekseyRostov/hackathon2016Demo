using Feedback.Core.ViewModels.Commands;
using Feedback.Core.ViewModels.Login.Commands;
using MvvmCross.Core.ViewModels;
using PropertyChanged;

namespace Feedback.Core.ViewModels.Login
{
    [ImplementPropertyChanged]
    public class LoginViewModel : MvxViewModel, ILoginViewModel
    {
        private IAsyncCommand _loginFacebookCommand;
        public IAsyncCommand LoginFacebookCommand
        {
            get
            {
                return _loginFacebookCommand ?? (_loginFacebookCommand = new LoginFacebookCommand(this));
            }
        }

        public bool IsLoggingIn { get; set; }
    }
}
