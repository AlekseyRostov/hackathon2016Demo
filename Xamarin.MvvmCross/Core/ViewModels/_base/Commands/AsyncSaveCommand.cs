using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Feedback.Core.Services;
using Microsoft.WindowsAzure.MobileServices;
using MvvmCross.Platform;

namespace Feedback.Core.ViewModels.Commands
{
    internal abstract class AsyncSaveCommand<TViewModel> : AsyncCommand
        where TViewModel : class, ISaveableViewModel
    {
        protected TViewModel ViewModel { get; private set; }

        protected IAuthenticationService _authenticationService { get { return Mvx.Resolve<IAuthenticationService>(); } }

        protected AsyncSaveCommand(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ViewModel.IsSaving = true;
            ViewModel.SaveFailureMessage = null;

            try
            {
                await ExecuteCoreAsync(parameter);
                ViewModel.SaveSucceeded = true;
            }
            catch(Exception ex)
            {
                if(!HandleException(ex)) throw;
            }
            finally
            {
                ViewModel.IsSaving = false;
            }
        }

        protected abstract Task ExecuteCoreAsync(object param);

        public virtual bool HandleException(Exception ex)
        {
            Debug.WriteLine(ex);

            var azureException = ex as MobileServiceInvalidOperationException;
            if(azureException?.Response?.StatusCode == HttpStatusCode.Unauthorized)
            {
                _authenticationService.LogoutAsync();
                return true;
            }

            if(ex is WebException)
            {
                ViewModel.SaveFailureMessage = "Failed to save data at this time due to network issue. Please check your internet connection and try again.";
                return true;
            }

            ViewModel.SaveFailureMessage = "Failed to load data at this time. Please try again later.";

            return true;
        }
    }
}