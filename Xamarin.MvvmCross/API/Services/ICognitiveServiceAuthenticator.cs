using System.Threading.Tasks;

namespace Feedback.API.Services
{
    public interface ICognitiveServiceAuthenticator
    {
        bool HasValidToken { get; }

        string Token { get; }

        Task AuthenticateAsync(string subscriptionKey);
    }
}