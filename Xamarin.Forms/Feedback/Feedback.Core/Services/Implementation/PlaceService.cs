using System.Collections.Generic;
using System.Net.Http;
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

        public async Task<Place> GetPlaceByBeaconAsync(BeaconModel beaconModel)
        {
            var parameters = new Dictionary<string, string>
                             {
                                 ["beaconId"] = beaconModel.UUID,
                                 ["major"] = beaconModel.Major.ToString(),
                                 ["minor"] = beaconModel.Minor.ToString()
                             };
            var result = await MobileService.InvokeApiAsync<Place>("PlaceItemCustom", HttpMethod.Get, parameters);
            return result;
        }
    }
}