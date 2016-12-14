using System.Threading.Tasks;
using TheRX.MVVM.VisualSupport;

namespace Feedback.UI.ViewModels.Base.Implementation
{
    internal abstract class AsyncCommand : Command, IAsyncCommand
    {
        public override async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}