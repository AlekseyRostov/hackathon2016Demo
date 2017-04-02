using System;
using Feedback.Core.ViewModels.Commands;

namespace Feedback.Core.ViewModels.Login
{
    public interface ILoginViewModel
    {
        IAsyncCommand LoginFacebookCommand { get; }

        bool IsLoggingIn { get; set; }
    }
}
