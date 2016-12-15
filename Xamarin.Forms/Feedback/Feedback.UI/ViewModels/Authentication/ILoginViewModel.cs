using Feedback.UI.ViewModels.Base;

namespace Feedback.UI.ViewModels.Authentication
{
    public interface ILoginViewModel
    {
        IAsyncCommand LoginFacebookCommand { get; }
        bool IsLoggingIn { get; set; }
    }
}