using Feedback.UI.ViewModels.Authentication;
using Microsoft.Practices.Unity;

namespace Feedback.UI.Core.Views.Authentication
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
            var viewModel = ServiceLocator.Instance.Resolve<ILoginViewModel>();
            BindingContext = viewModel;
        }
    }
}