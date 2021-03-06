﻿using System;
using Feedback.UI.ViewModels.Feedbacks;
using Microsoft.Practices.Unity;

namespace Feedback.UI.Core.Views.Feedbacks
{
    public partial class FeedbacksPage
    {
        private readonly IFeedbacksViewModel _viewModel;

        public FeedbacksPage(string placeId, string placeName)
        {
            InitializeComponent();
            _viewModel = ServiceLocator.Instance.Resolve<IFeedbacksViewModel>();
            _viewModel.PlaceId = placeId;
            _viewModel.PlaceName = placeName;
            Title = placeName;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadCommand.Execute(null);
        }

        private async void AddFeedbackClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FeedbackPage(_viewModel.PlaceId, _viewModel.PlaceName));
        }
    }
}