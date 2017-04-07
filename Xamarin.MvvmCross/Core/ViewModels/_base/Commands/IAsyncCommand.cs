using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;

namespace Feedback.Core.ViewModels.Commands
{
    public interface IAsyncCommand : IMvxCommand
    {
        Task ExecuteAsync(object parameter);
    }
}