using System.ComponentModel;
using Feedback.UI.ViewModels.Feedbacks;
using Microsoft.Practices.Unity;
using Strings = Feedback.UI.Resources.Strings.Feedbacks.Common;

namespace Feedback.UI.Core.Views.Feedbacks
{
    public partial class FeedbackPage
    {
        public IFeedbackViewModel ViewModel { get; }

        public FeedbackPage(string placeId, string placeName)
        {
            InitializeComponent();
            ViewModel = ServiceLocator.Instance.Resolve<IFeedbackViewModel>();
            ViewModel.PlaceId = placeId;
            Title = placeName;
            ViewModel.UserEmail = "test@mail.com";
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.PropertyChanged -= ViewModelOnPropertyChanged;
        }

        private async void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IFeedbackViewModel.SaveSucceeded) ||
               e.PropertyName == nameof(IFeedbackViewModel.SaveFailureMessage))
            {
                if(ViewModel.SaveSucceeded)
                {
                    await Navigation.PopAsync();
                }
                else if(!string.IsNullOrEmpty(ViewModel.SaveFailureMessage))
                {
                    await DisplayAlert(Strings.Error, Strings.SaveFailure, Strings.Ok);
                }
            }
        }
    }
}