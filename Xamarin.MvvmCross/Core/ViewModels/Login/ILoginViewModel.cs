using System;
using Feedback.Core.ViewModels.Commands;
using MvvmCross.Core.ViewModels;

namespace Feedback.Core.ViewModels.Login
{
    public interface ILoginViewModel : IMvxViewModel
    {
        IAsyncCommand LoginFacebookCommand { get; }

        bool IsLoggingIn { get; set; }
    }
}
