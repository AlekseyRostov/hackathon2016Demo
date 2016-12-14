using System;
using WindowsAzure.Messaging;
using Feedback.Core;
using Feedback.UI.Core;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;
using Xamarin.Forms;
using Strings = Feedback.UI.Resources.Strings.Feedbacks.Common;

namespace Feedback.UI.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private const string TemplateBodyApns = "{\"aps\":{\"alert\":\"$(messageParam)\"}}";
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            CurrentPlatform.Init();
            ServiceLocator.Instance.RegisteriOSDependencies();
            LoadApplication(new App());
            RegisterPushNotifications();
            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            var connectionString = SBConnectionString.CreateListenAccess(new NSUrl(Constants.NotificationHubUrl),
                                                                         Constants.NotificationHubKey);
            var hub = new SBNotificationHub(connectionString, Constants.NotificationHubName);

            hub.UnregisterAllAsync(deviceToken, error =>
                                                {
                                                    if(error != null)
                                                    {
                                                        Console.WriteLine("Error calling Unregister: {0}", error.ToString());
                                                        return;
                                                    }

                                                    NSSet tags = null; // create tags if you want
                                                    hub.RegisterNativeAsync(deviceToken, tags, errorCallback =>
                                                                                               {
                                                                                                   if(errorCallback != null)
                                                                                                       Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
                                                                                               });
                                                });
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            var notification = (NSDictionary) userInfo.ObjectForKey(new NSString("aps"));

            string alert = string.Empty;
            if(notification.ContainsKey(new NSString("alert")))
            {
                alert = ((NSString) notification[new NSString("alert")]).ToString();
            }

            // Show alert
            if(!string.IsNullOrEmpty(alert))
            {
                UIAlertView avAlert = new UIAlertView(Strings.NewFeedback, alert, null, "OK", null);
                avAlert.Show();
            }
        }

        private void RegisterPushNotifications()
        {
            var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert, new NSSet());

            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
        }
    }
}