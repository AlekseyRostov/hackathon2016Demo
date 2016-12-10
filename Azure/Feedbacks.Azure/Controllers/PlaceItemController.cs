using Feedbacks.Azure.DataObjects;
using Feedbacks.Azure.Models;
using Microsoft.Azure.Mobile.Server;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Feedbacks.Azure.Controllers
{
    /// <summary>
    /// Метод получения места по идентификатору маяка
    /// </summary>
    public class PlaceItemController : TableController<PlaceItem>
    {
        /// <summary>
        ///  GET api/placeitem
        /// </summary>
        /// <returns>PlaceItem</returns>
        public PlaceItem Get(Guid beaconId, int major, int minor)
        {
            return Query().FirstOrDefault(x => x.BeaconUUID == beaconId && x.BeaconMajor == major && x.BeaconMinor == minor);
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
