using System.ComponentModel;
using Feedback.UI.ViewModels.Feedbacks;
using Microsoft.Practices.Unity;
using Strings = Feedback.UI.Resources.Strings.Feedbacks.Common;

namespace Feedback.UI.Core.Views.Feedbacks
{
    public partial class AddFeedbackPage
    {
        private readonly IAddFeedbackViewModel _viewModel;

        public AddFeedbackPage(string placeId)
        {
            InitializeComponent();
            _viewModel = ServiceLocator.Instance.Resolve<IAddFeedbackViewModel>();
            _viewModel.PlaceId = placeId;
            //TODO:Add actual mail
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
            if(e.PropertyName == nameof(IAddFeedbackViewModel.IsLoading))
            {
                if(string.IsNullOrEmpty(_viewModel.LoadFailureMessage))
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert(Strings.Error, Strings.SaveFailure, Strings.Ok);
                }
            }
        }
    }
}