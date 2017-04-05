using Feedback.API;
using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using UIKit;
using WindowsAzure.Messaging;
using Feedback.iOS.Presenter;

namespace Feedback.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        private const string TemplateBodyApns = "{\"aps\":{\"alert\":\"$(messageParam)\"}}";

        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, new FeedbackIosViewPresenter(this, Window));
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            RegisterPushNotifications();

            Window.MakeKeyAndVisible();

            return true;
        }

        #region Push notifications

        private void RegisterPushNotifications()
        {
            var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert, new NSSet());

            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            var connectionString = SBConnectionString.CreateListenAccess(new NSUrl(Constants.NotificationHubUrl),
                                                                         Constants.NotificationHubKey);
            var hub = new SBNotificationHub(connectionString, Constants.NotificationHubName);

            hub.UnregisterAllAsync(deviceToken, error =>
                                                {
                                                    if (error != null)
                                                    {
                                                        System.Diagnostics.Debug.WriteLine("Error calling Unregister: {0}", error.ToString());
                                                        return;
                                                    }

                                                    NSSet tags = null; // create tags if you want
                                                    hub.RegisterNativeAsync(deviceToken, tags, errorCallback =>
                                                                                               {
                                                                                                   if (errorCallback != null)
                                                                                                       System.Diagnostics.Debug.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
                                                                                               });
                                                });
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
        {
            var notification = (NSDictionary)userInfo.ObjectForKey(new NSString("aps"));

            string alert = string.Empty;
            if (notification.ContainsKey(new NSString("alert")))
            {
                alert = ((NSString)notification[new NSString("alert")]).ToString();
            }

            // Show alert
            if (!string.IsNullOrEmpty(alert))
            {
                UIAlertView avAlert = new UIAlertView("New feedback added", alert, null, "OK", null);
                avAlert.Show();
            }
        }

        #endregion
    }
}

