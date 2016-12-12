using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Feedback.Core.Services.Implementation
{
    public class FeedbackService : BaseAzureService, IFeedbackService
    {
        public async Task<IEnumerable<Entities.Feedback>> GetFeedbacksAsync(string placeId, int skip = 0, int take = 100)
        {
            var feedbacks = MobileService.GetTable<Entities.Feedback>();
            var result = await feedbacks.Where(feedback => feedback.PlaceId == placeId).Skip(skip).Take(100).ToListAsync();
            return result;
        }

        public async Task SaveFeedbackAsync(string placeId, string userEmail, string text)
        {
            var feedbacks = MobileService.GetTable<Entities.Feedback>();
            await feedbacks.InsertAsync(new Entities.Feedback
                                        {
                                            PlaceId = placeId,
                                            UserEmail = userEmail,
                                            Text = text,
                                            CreationDate = DateTimeOffset.Now
                                        });
        }
    }
}