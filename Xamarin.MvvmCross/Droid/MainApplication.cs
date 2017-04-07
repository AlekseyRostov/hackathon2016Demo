using System;
using Android.App;
using Feedback.Droid.Services;
using Android.Runtime;

namespace Feedback.Droid
{
    [Application]
    public partial class MainApplication : Application
    {
        public static MainApplication Instance { get; private set; }

        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
            : base (handle, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            Instance = this;

            RegisterPushNotifications();
        }

        private void RegisterPushNotifications()
        {
            try
            {
                GcmService.Initialize(this);
                GcmService.Register(this);
            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            }
            catch (Exception e)
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
