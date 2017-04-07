using System;
using System.Threading.Tasks;
using Feedback.Core.ViewModels.Commands;

namespace Feedback.Core.ViewModels.Places.Commands
{
    internal class LogoutCommand : AsyncLoadCommand<IPlacesViewModel>
    {
        public LogoutCommand(IPlacesViewModel viewModel)
            : base (viewModel)
        {
        }

        protected override Task ExecuteCoreAsync(object param)
        {
            return AuthenticationService.LogoutAsync();
        }
    }
}
