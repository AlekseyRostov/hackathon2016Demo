using System.Threading.Tasks;

namespace Feedback.Core.Services.Cognitive
{
    public interface ISpeechService
    {
        Task<string> SpeechToTextAsync(string recordingPath, string locale);
    }
}