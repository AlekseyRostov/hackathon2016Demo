using Android.App;
using Android.Content.PM;
using Android.OS;
using Feedback.UI.Core;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace Feedback.Droid
{
    [Activity(Label = "@string/app_name",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            CurrentPlatform.Init();
            Forms.Init(this, bundle);
            ServiceLocator.Instance.RegisterDroidDependencies();
            LoadApplication(new App());
        }
    }
}