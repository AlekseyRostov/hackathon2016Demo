using Feedbacks.Azure.DataObjects;
using Feedbacks.Azure.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.NotificationHubs;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;

namespace Feedbacks.Azure.Controllers
{
    /// <summary>
    /// Работа с отзывами
    /// </summary>
    public class FeedbackItemController : TableController<FeedbackItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<FeedbackItem>(context, Request);
        }

        // GET tables/FeedbackItem
        public IQueryable<FeedbackItem> GetAllFeedbackItems()
        {
            return Query();
        }

        // GET tables/FeedbackItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<FeedbackItem> GetFeedbackItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/FeedbackItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<FeedbackItem> PatchFeedbackItem(string id, Delta<FeedbackItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/FeedbackItem
        [Authorize]
        public async Task<IHttpActionResult> PostFeedbackItem(FeedbackItem item)
        {
            var current = await InsertAsync(item);
            await SendPush(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        private async Task SendPush(FeedbackItem item)
        {
            // Get the settings for the server project.
            HttpConfiguration config = this.Configuration;

            MobileAppSettingsDictionary settings =
                this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            // Get the Notification Hubs credentials for the Mobile App.
            string notificationHubName = settings.NotificationHubName;
            string notificationHubConnection = settings
                .Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            // Create a new Notification Hub client.
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            // iOS payload
            var appleNotificationPayload = "{\"aps\":{\"alert\":\"" + item.Text + "\"}}";

            // Google 
            var googleNotificationPayload = "{\"data\":{\"message\":\"" + item.Text + "\"}}";


            try
            {
                // Send the push notification and log the results.
                var result = await hub.SendAppleNativeNotificationAsync(appleNotificationPayload);

                // Send the push notification and log the results.
                var resultGoogle = await hub.SendGcmNativeNotificationAsync(googleNotificationPayload);

                // Write the success result to the logs.
                config.Services.GetTraceWriter().Info(result.State.ToString());
                // Write the success result to the logs.
                config.Services.GetTraceWriter().Info(resultGoogle.State.ToString());
            }
            catch (System.Exception ex)
            {
                // Write the failure result to the logs.
                config.Services.GetTraceWriter()
                    .Error(ex.Message, null, "Push.SendAsync Error");
            }
        }

        // DELETE tables/FeedbackItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFeedbackItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}