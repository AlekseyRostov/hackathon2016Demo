using System.Text;
using WindowsAzure.Messaging;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V7.App;
using Android.Util;
using Gcm.Client;
using CoreConstants = Feedback.Core.Constants;
using Strings = Feedback.UI.Resources.Strings.Feedbacks.Common;

[assembly:Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly:UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly:UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

namespace Feedback.UI.Droid.Services
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] {Constants.INTENT_FROM_GCM_MESSAGE}, Categories = new[] {"@PACKAGE_NAME@"})]
    [IntentFilter(new[] {Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK}, Categories = new[] {"@PACKAGE_NAME@"})]
    [IntentFilter(new[] {Constants.INTENT_FROM_GCM_LIBRARY_RETRY}, Categories = new[] {"@PACKAGE_NAME@"})]
    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static readonly string[] SenderIds = {"558275389429"};
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        private static NotificationHub _hub;

        public GcmService()
            : base(PushHandlerBroadcastReceiver.SenderIds)
        {
        }

        public static void Initialize(Context context)
        {
            GcmClient.CheckDevice(context);
            GcmClient.CheckManifest(context);
            var connectionString = ConnectionString
                .CreateUsingSharedAccessKeyWithListenAccess(new Java.Net.URI(CoreConstants.NotificationHubUrl),
                                                            CoreConstants.NotificationHubKey);
            _hub = new NotificationHub(CoreConstants.NotificationHubName, connectionString, context);
        }

        public static void Register(Context context)
        {
            // Makes this easier to call from our Activity
            GcmClient.Register(context, PushHandlerBroadcastReceiver.SenderIds);
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            _hub.Register(registrationId);
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var msg = new StringBuilder();

            if(intent?.Extras != null)
            {
                foreach(var key in intent.Extras.KeySet())
                {
                    msg.AppendLine(key + "=" + intent.Extras.Get(key));
                }
            }

            var message = intent?.Extras?.GetString("message");
            if(!string.IsNullOrEmpty(message))
            {
                CreateNotification(Strings.NewFeedback, message);
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "Unregistered RegisterationId : " + registrationId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "GCM Error: " + errorId);
        }

        private void CreateNotification(string title, string desc)
        {
            //Create notification
            var notificationManager = (NotificationManager) GetSystemService(NotificationService);

            //Create an intent to show ui
            var uiIntent = new Intent(this, typeof(MainActivity));

            //Use Notification Builder
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);

            //Create the notification
            //we use the pending intent, passing our ui intent over which will get called
            //when the notification is tapped.
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                                      .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                                      .SetTicker(title)
                                      .SetContentTitle(title)
                                      .SetContentText(desc)

                //Set the notification sound
                                      .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))

                //Auto cancel will remove the notification once the user touches it
                                      .SetAutoCancel(true).Build();

            //Show the notification
            notificationManager.Notify(1, notification);
        }
    }
}