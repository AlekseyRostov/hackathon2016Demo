using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Feedback.Core.Services;
using Microsoft.WindowsAzure.MobileServices;
using Strings = Feedback.UI.Resources.Strings.Common.Common;

namespace Feedback.UI.ViewModels.Base.Implementation
{
    internal abstract class AsyncLoadCommand : AsyncCommand
    {
        private readonly ILoadableViewModel _viewModel;
        private readonly IAuthenticationService _authenticationService;

        protected AsyncLoadCommand(ILoadableViewModel viewModel, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _viewModel = viewModel;
            viewModel.IsLoaded = false;
            viewModel.IsEmpty = true;
        }

        public override async Task ExecuteAsync(object param)
        {
            _viewModel.IsLoaded = false;
            _viewModel.IsLoading = true;
            _viewModel.LoadFailureMessage = null;

            try
            {
                await ExecuteCoreAsync(param);
                _viewModel.IsLoaded = true;
            }
            catch(Exception ex)
            {
                if(!HandleException(ex)) throw;
            }
            finally
            {
                _viewModel.IsLoading = false;
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
                _viewModel.LoadFailureMessage = Strings.LoadDataNetworkFailure;
                return true;
            }

            _viewModel.LoadFailureMessage = Strings.LoadDataUnknownFailure;
            return true;
        }
    }
}