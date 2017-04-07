using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Feedback.Core.Services;
using Feedback.Core.ViewModels.Commands;
using MvvmCross.Platform;

namespace Feedback.Core.ViewModels.Login.Commands
{
    internal abstract class BaseLoginCommand : AsyncCommand
    {
        protected ILoginViewModel ViewModel { get; }

        protected IAuthenticationService AuthenticationService { get { return Mvx.Resolve<IAuthenticationService>(); } }

        protected BaseLoginCommand(ILoginViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ViewModel.IsLoggingIn = true;
            try
            {
                await LoginAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                ViewModel.IsLoggingIn = false;
            }
        }

        public abstract Task LoginAsync();
    }
}