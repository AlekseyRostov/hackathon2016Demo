using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Feedback.Droid
{
    [Activity(Label = "@string/app_name"
            , Icon = "@drawable/icon"
            , Theme = "@style/MainTheme"    
            , MainLauncher = true
            , NoHistory = true
            , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenAppCompatActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}
