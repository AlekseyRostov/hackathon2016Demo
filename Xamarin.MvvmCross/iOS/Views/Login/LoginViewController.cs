using MvvmCross.iOS.Views;
using Feedback.Core.ViewModels.Login;
using MvvmCross.Binding.BindingContext;

namespace Feedback.iOS.Views.Login
{
    public partial class LoginViewController : MvxViewController<ILoginViewModel>
    {
        public LoginViewController()
            : base("LoginViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _loginBtn.SetTitle("Login with Facebook", UIKit.UIControlState.Normal);

            var set = this.CreateBindingSet<LoginViewController, ILoginViewModel>();
            set.Bind(_loginBtn).To(vm => vm.LoginFacebookCommand);
            set.Apply();
        }
    }
}

