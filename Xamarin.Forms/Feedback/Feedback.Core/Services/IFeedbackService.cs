using System.Collections.Generic;
using System.Threading.Tasks;

namespace Feedback.Core.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Entities.Feedback>> GetFeedbacksAsync(string placeId, int skip = 0, int take = 100);
    }
}