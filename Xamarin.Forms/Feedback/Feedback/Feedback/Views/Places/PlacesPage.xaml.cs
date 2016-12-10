using Feedback.UI.ViewModels.Places;
using Microsoft.Practices.Unity;

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
            _viewModel.LoadCommand?.Execute(null);
        }
    }
}