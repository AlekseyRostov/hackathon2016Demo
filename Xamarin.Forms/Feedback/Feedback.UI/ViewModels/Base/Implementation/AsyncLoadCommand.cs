using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Strings = Feedback.UI.Resources.Strings.Common.Common;

namespace Feedback.UI.ViewModels.Base.Implementation
{
    internal abstract class AsyncLoadCommand : AsyncCommand
    {
        private readonly ILoadableViewModel _viewModel;

        protected AsyncLoadCommand(ILoadableViewModel viewModel)
        {
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