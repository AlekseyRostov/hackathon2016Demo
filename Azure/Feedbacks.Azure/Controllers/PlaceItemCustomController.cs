using Feedbacks.Azure.DataObjects;
using Feedbacks.Azure.Models;
using Microsoft.Azure.Mobile.Server.Config;
using System;
using System.Linq;
using System.Web.Http;

namespace Feedbacks.Azure.Controllers
{
    /// <summary>
    /// Список мест
    /// </summary>
    [MobileAppController]
    public class PlaceItemCustomController : ApiController
    {
        /// <summary>
        /// Получение места по информации от маяка
        /// </summary>
        /// <param name="beaconId"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <returns></returns>

        public PlaceItem GetPlaceItem(Guid beaconId, int major, int minor)
        {
            using (var context = new MobileServiceContext())
            {
                return context.PlaceItems.FirstOrDefault(x => x.BeaconUUID == beaconId && x.BeaconMajor == major && x.BeaconMinor == minor);
            }
        }
    }
}
