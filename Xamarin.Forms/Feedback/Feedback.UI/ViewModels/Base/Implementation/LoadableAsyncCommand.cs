using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Strings = Feedback.UI.Resources.Strings.Common.Common;

namespace Feedback.UI.ViewModels.Base.Implementation
{
    internal abstract class LoadableAsyncCommand : AsyncCommand
    {
        private readonly ILoadableViewModel _viewModel;

        protected LoadableAsyncCommand(ILoadableViewModel viewModel)
        {
            _viewModel = viewModel;
            viewModel.IsLoaded = false;
            viewModel.IsEmpty = true;
        }

        public override async Task ExecuteAsync(object param, CancellationToken token = default(CancellationToken))
        {
            _viewModel.IsLoaded = false;
            _viewModel.IsLoading = true;
            _viewModel.LoadFailureMessage = null;

            try
            {
                await ExecuteCoreAsync(param, token);
                _viewModel.IsLoaded = true;
            }
            catch(Exception ex)
            {
                if(!HandleException(ex, token)) throw;
            }
            finally
            {
                _viewModel.IsLoading = false;
            }
        }

        protected abstract Task ExecuteCoreAsync(object param, CancellationToken token = default(CancellationToken));

        public virtual bool HandleException(Exception ex, CancellationToken token)
        {
            //Ignore operation cancellations
            if(ex is OperationCanceledException)
            {
                return true;
            }

            Debug.WriteLine(ex);
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