using Feedbacks.Azure.DataObjects;
using Feedbacks.Azure.Models;
using Microsoft.Azure.Mobile.Server;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Feedbacks.Azure.Controllers
{
    /// <summary>
    /// Список мест
    /// </summary>

    public class PlaceItemController : TableController<PlaceItem>
    {

        /// <summary>
        ///  GET GetAllPlaceItems api/placeitem
        /// </summary>
        /// <returns>PlaceItem</returns>
        public IQueryable<PlaceItem> GetAllPlaceItems()
        {
            return Query();
        }


        /// <summary>
        ///  POST tables/PlaceItem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> PostPlaceItem(PlaceItem item)
        {
            PlaceItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<PlaceItem>(context, Request);
        }

    }
}
