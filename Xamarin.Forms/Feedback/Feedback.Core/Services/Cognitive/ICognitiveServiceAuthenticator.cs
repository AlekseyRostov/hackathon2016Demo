using System.Threading.Tasks;

namespace Feedback.Core.Services.Cognitive
{
    public interface ICognitiveServiceAuthenticator
    {
        bool HasValidToken { get; }
        string Token { get; }
        Task AuthenticateAsync(string subscriptionKey);
    }
}