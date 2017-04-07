using System.Threading.Tasks;

namespace Feedback.Core.Services
{
    public interface ISpeechService
    {
        Task<string> SpeechToTextAsync(string recordingPath, string locale);
    }
}