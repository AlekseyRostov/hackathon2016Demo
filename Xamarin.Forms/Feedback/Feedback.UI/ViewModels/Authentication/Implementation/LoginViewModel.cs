using Feedback.UI.ViewModels.Base;
using PropertyChanged;
using TheRX.MVVM.VisualSupport;

namespace Feedback.UI.ViewModels.Authentication.Implementation
{
    [ImplementPropertyChanged]
    internal class LoginViewModel : ViewModel, ILoginViewModel
    {
        public LoginViewModel(AuthenticationFactory factory)
        {
            LoginFacebookCommand = factory.GetLoginFacebookCommand(this);
        }

        public IAsyncCommand LoginFacebookCommand { get; }
        public bool IsLoggingIn { get; set; }
    }
}