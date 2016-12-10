using Feedbacks.Azure.DataObjects;
using Feedbacks.Azure.Models;
using Microsoft.Azure.Mobile.Server;
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
        public async Task<IHttpActionResult> PostFeedbackItem(FeedbackItem item)
        {
            FeedbackItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/FeedbackItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFeedbackItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}