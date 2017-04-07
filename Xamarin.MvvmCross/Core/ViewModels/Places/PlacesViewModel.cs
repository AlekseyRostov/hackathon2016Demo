using System.Collections.ObjectModel;
using System.Windows.Input;
using Feedback.API.Entities;
using Feedback.Core.ViewModels.Commands;
using Feedback.Core.ViewModels.Feedbacks;
using Feedback.Core.ViewModels.Places.Commands;
using MvvmCross.Core.ViewModels;
using PropertyChanged;

namespace Feedback.Core.ViewModels.Places
{
    [ImplementPropertyChanged]
    internal class PlacesViewModel : BaseLoadableViewModel, IPlacesViewModel
    {
        #region Commands

        private IAsyncCommand _loadCommand;
        public override IAsyncCommand LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = new LoadPlacesCommand(this));
            }
        }

        private IAsyncCommand _logoutCommand;
        public IAsyncCommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new LogoutCommand(this));
            }
        }

        private ICommand _selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                return _selectionChangedCommand ?? (_selectionChangedCommand = new MvxCommand<Place>(OnCommandExecute));
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<Place> Places { get; set; }

        #endregion

        #region Private 

        private void OnCommandExecute(Place item)
        {
            ShowViewModel<FeedbacksViewModel>(new { id = item.Id, name = item.Name });
        }

        #endregion

        #region Public

        public override void Start()
        {
            base.Start();

            LoadCommand.Execute(null);
        }

        #endregion
    }
}