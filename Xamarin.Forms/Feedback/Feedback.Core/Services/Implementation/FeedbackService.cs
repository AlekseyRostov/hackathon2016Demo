using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Feedback.Core.Services.Implementation
{
    public class FeedbackService : BaseAzureService, IFeedbackService
    {
        public async Task<IEnumerable<Entities.Feedback>> GetFeedbacksAsync(string placeId, int skip = 0, int take = 100, CancellationToken token = new CancellationToken())
        {
            var feedbacks = MobileService.GetTable<Entities.Feedback>();
            var result = await feedbacks.Where(feedback => feedback.PlaceId == placeId).Skip(skip).Take(100).ToListAsync();
            return result;
        }
    }
}