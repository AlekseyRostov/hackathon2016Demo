using System.ComponentModel;
using Feedback.UI.ViewModels.Feedbacks;
using Microsoft.Practices.Unity;
using Strings = Feedback.UI.Resources.Strings.Feedbacks.Common;

namespace Feedback.UI.Core.Views.Feedbacks
{
    public partial class FeedbackPage
    {
        private readonly IFeedbackViewModel _viewModel;

        public FeedbackPage(string placeId, string placeName)
        {
            InitializeComponent();
            _viewModel = ServiceLocator.Instance.Resolve<IFeedbackViewModel>();
            _viewModel.PlaceId = placeId;
            Title = placeName;
            _viewModel.UserEmail = "test@mail.com";
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }

        private async void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IFeedbackViewModel.SaveSucceeded) ||
               e.PropertyName == nameof(IFeedbackViewModel.SaveFailureMessage))
            {
                if(_viewModel.SaveSucceeded)
                {
                    await Navigation.PopAsync();
                }
                else if(!string.IsNullOrEmpty(_viewModel.SaveFailureMessage))
                {
                    await DisplayAlert(Strings.Error, Strings.SaveFailure, Strings.Ok);
                }
            }
        }
    }
}