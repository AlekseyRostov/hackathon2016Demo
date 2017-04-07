using Android.App;
using Android.Content.PM;

namespace Feedback.Droid
{
    [Activity(Label = "@string/app_name"
            , Icon = "@drawable/icon"
            , Theme = "@style/MainTheme"    
            , MainLauncher = true
            , NoHistory = true
            , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvvmCross.Droid.Support.V7.AppCompat.MvxSplashScreenAppCompatActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
