using Feedback.Core.Entities;
using Feedback.UI.Core.Views.Feedbacks;
using Feedback.UI.ViewModels.Places;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace Feedback.UI.Core.Views.Places
{
    public partial class PlacesPage
    {
        private readonly IPlacesViewModel _viewModel;

        public PlacesPage()
        {
            InitializeComponent();
            _viewModel = ServiceLocator.Instance.Resolve<IPlacesViewModel>();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadCommand.Execute(null);
        }

        private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(PlacesListView.SelectedItem != null)
            {
                var place = (Place) PlacesListView.SelectedItem;
                PlacesListView.SelectedItem = null;
                await Navigation.PushAsync(new FeedbacksPage(place.Id));
            }
        }
    }
}