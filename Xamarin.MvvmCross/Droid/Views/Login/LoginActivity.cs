using Android.App;
using Feedback.Core.ViewModels.Login;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Feedback.Droid.Views.Login
{
    [Activity]
    public class LoginActivity : MvxAppCompatActivity<ILoginViewModel>
    {
        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.LoginView);
        }
    }
}
