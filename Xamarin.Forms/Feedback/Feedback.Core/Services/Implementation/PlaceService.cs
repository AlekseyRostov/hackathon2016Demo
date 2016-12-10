using System.Collections.Generic;
using System.Threading.Tasks;
using Feedback.Core.Entities;

namespace Feedback.Core.Services.Implementation
{
    public class PlaceService : BaseAzureService, IPlaceService
    {
        public async Task<IEnumerable<Place>> GetPlacesAsync(int skip = 0, int take = 100)
        {
            var places = MobileService.GetTable<Place>();
            var result = await places.Skip(skip).Take(take).ToListAsync();
            return result;
        }
    }
}