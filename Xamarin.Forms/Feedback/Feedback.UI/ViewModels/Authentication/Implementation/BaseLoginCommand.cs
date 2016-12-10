using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Feedback.Core.Services;
using Feedback.UI.ViewModels.Base.Implementation;

namespace Feedback.UI.ViewModels.Authentication.Implementation
{
    internal abstract class BaseLoginCommand : AsyncCommand
    {
        protected IAuthenticationService AuthenticationService { get; }
        protected LoginViewModel ViewModel { get; }

        public BaseLoginCommand(LoginViewModel viewModel, IAuthenticationService authenticationService)
        {
            ViewModel = viewModel;
            AuthenticationService = authenticationService;
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