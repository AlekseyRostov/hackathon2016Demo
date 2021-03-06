﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Feedback.Core.Messages;
using Feedback.Core.ViewModels.Commands;
using Feedback.Core.ViewModels.Feedbacks.Commands;
using Feedback.Core.ViewModels.Feedbacks.Feedback;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace Feedback.Core.ViewModels.Feedbacks
{
    internal class FeedbacksViewModel : BaseLoadableViewModel, IFeedbacksViewModel
    {
        #region Fields

        private MvxSubscriptionToken _feedbackSavedToken;

        #endregion

        #region Commands

        private IAsyncCommand _loadCommand;
        public override IAsyncCommand LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = new LoadFeedbacksCommand(this));
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new MvxCommand(OnCommandExecute));
            }
        }

        #endregion

        #region Properties

        public string PlaceId { get; set; }

        public string PlaceName { get; set; }

        public ObservableCollection<API.Entities.Feedback> Feedbacks { get; set; }

        #endregion

        private void OnCommandExecute()
        {
            var mvxMessenger = Mvx.Resolve<IMvxMessenger>();

            if (_feedbackSavedToken != null)
                mvxMessenger.Unsubscribe<FeedbackSavedMessage>(_feedbackSavedToken);

            _feedbackSavedToken = mvxMessenger.Subscribe<FeedbackSavedMessage>(msg => Start());

            ShowViewModel<FeedbackViewModel>(new { id = PlaceId, name = PlaceName });
        }

        public void Init (string id, string name)
        {
            PlaceId = id;
            PlaceName = name;
        }

        public override void Start()
        {
            base.Start();

            LoadCommand.Execute(null);
        }
    }
}