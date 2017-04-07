using System;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace Feedback.Core.ViewModels.Commands
{
    internal abstract class AsyncCommand : MvxCommandBase, IAsyncCommand
    {
        private readonly Func<bool> _canExecute;

        protected AsyncCommand()
        {
            
        }

        protected AsyncCommand(Func<bool> canExecute)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute()
        {
            return this.CanExecute(null);
        }

        public virtual bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public async void Execute(object parameter)
        {
            if (CanExecute(parameter))
                await ExecuteAsync(parameter).ConfigureAwait(true);
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}