using Feedback.UI.ViewModels.Feedbacks;
using Microsoft.Practices.Unity;

namespace Feedback.UI.Core.Views.Feedbacks
{
    public partial class AddFeedbackPage
    {
        public AddFeedbackPage(string placeId)
        {
            InitializeComponent();
            var viewModel = ServiceLocator.Instance.Resolve<IAddFeedbackViewModel>();
            viewModel.PlaceId = placeId;
            //TODO:Add actual mail
            viewModel.UserEmail = "test@mail.com";
            BindingContext = viewModel;
        }
    }
}