using System.Threading.Tasks;
using System.Windows.Input;

namespace Feedback.UI.ViewModels.Base
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object param);
    }
}