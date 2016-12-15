using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Feedback.Core.Services;
using Microsoft.WindowsAzure.MobileServices;
using Strings = Feedback.UI.Resources.Strings.Common.Common;

namespace Feedback.UI.ViewModels.Base.Implementation
{
    internal abstract class AsyncSaveCommand : AsyncCommand
    {
        private readonly ISaveableViewModel _viewModel;
        private readonly IAuthenticationService _authenticationService;

        protected AsyncSaveCommand(ISaveableViewModel viewModel, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object param)
        {
            _viewModel.IsSaving = true;
            _viewModel.SaveFailureMessage = null;

            try
            {
                await ExecuteCoreAsync(param);
                _viewModel.SaveSucceeded = true;
            }
            catch(Exception ex)
            {
                if(!HandleException(ex)) throw;
            }
            finally
            {
                _viewModel.IsSaving = false;
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
                _viewModel.SaveFailureMessage = Strings.SaveDataNetworkFailure;
                return true;
            }

            _viewModel.SaveFailureMessage = Strings.SaveDataUnknownFailure;
            return true;
        }
    }
}