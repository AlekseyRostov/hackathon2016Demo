using Feedback.API;
using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Platform;
using UIKit;
using WindowsAzure.Messaging;
using Feedback.iOS.Presenter;
using UserNotifications;
using System;

namespace Feedback.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate, IUNUserNotificationCenterDelegate
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
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                UNUserNotificationCenter.Current.Delegate = this;

                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) =>
                                         {
                                             if (approved)
                                             {

                                                 InvokeOnMainThread(() =>
                                                 {
                                                     UIApplication.SharedApplication.RegisterForRemoteNotifications();
                                                 });
                                             }

                                         });
            }
            else
            {
                var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert, new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
        }

        private void OnNotificationReceived(NSDictionary userInfo)
        {
            var notification = (NSDictionary)userInfo.ObjectForKey(new NSString("aps"));

            string alert = string.Empty;
            if (notification.ContainsKey(new NSString("alert")))
                alert = ((NSString)notification[new NSString("alert")]).ToString();

            // Show alert
            if (!string.IsNullOrEmpty(alert))
            {
                UIAlertView avAlert = new UIAlertView("New feedback added", alert, null, "OK", null);
                avAlert.Show();
            }
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

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            OnNotificationReceived(userInfo);
        }

        #region IUNUserNotificationCenterDelegate implementation

        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var userInfo = response.Notification.Request.Content.UserInfo;

            if (UIApplication.SharedApplication.ApplicationState == UIApplicationState.Active ||
                UIApplication.SharedApplication.ApplicationState == UIApplicationState.Inactive)
                OnNotificationReceived(userInfo);

            completionHandler();
        }

        #endregion

        #endregion
    }
}

