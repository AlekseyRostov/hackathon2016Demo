using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Feedback.Core.Services;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;

namespace Feedback.Core.ViewModels.Commands
{
    internal abstract class AsyncLoadCommand<TViewModel> : AsyncCommand
        where TViewModel : class, ILoadableViewModel
    {
        protected TViewModel ViewModel { get; private set; }

        protected IAuthenticationService AuthenticationService { get { return Mvx.Resolve<IAuthenticationService>(); } }

        protected AsyncLoadCommand(TViewModel viewModel)
        {
            ViewModel = viewModel;

            ViewModel.IsLoaded = false;
            ViewModel.IsEmpty = true;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ViewModel.IsLoaded = false;
            ViewModel.IsLoading = true;
            ViewModel.LoadFailureMessage = null;

            try
            {
                await ExecuteCoreAsync(parameter);
                ViewModel.IsLoaded = true;
            }
            catch(Exception ex)
            {
                if(!HandleException(ex)) throw;
            }
            finally
            {
                ViewModel.IsLoading = false;
            }
        }

        protected abstract Task ExecuteCoreAsync(object param);

        public virtual bool HandleException(Exception ex)
        {
            Debug.WriteLine(ex);

            var azureException = ex as MobileServiceInvalidOperationException;
            if(azureException?.Response?.StatusCode == HttpStatusCode.Unauthorized)
            {
                AuthenticationService.LogoutAsync();
                return true;
            }

            if(ex is WebException)
            {
                ViewModel.LoadFailureMessage = "Failed to load data at this time due to network issue. Please check your internet connection and try again.";
                return true;
            }

            ViewModel.LoadFailureMessage = "Failed to load data at this time. Please try again later.";
            return true;
        }
    }
}