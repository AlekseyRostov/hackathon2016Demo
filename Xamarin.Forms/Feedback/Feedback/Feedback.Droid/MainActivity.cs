using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Feedback.UI.Core;
using Feedback.UI.Droid.Resources;
using Feedback.UI.Droid.Services;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace Feedback.UI.Droid
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
            RegisterPushNotifications();
        }

        private void RegisterPushNotifications()
        {
            try
            {
                GcmService.Initialize(this);
                GcmService.Register(this);
            }
            catch(Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            }
            catch(Exception e)
            {
                CreateAndShowDialog(e.Message, "Error");
            }
        }

        private void CreateAndShowDialog(String message, String title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }
    }
}