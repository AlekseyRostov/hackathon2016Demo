using System.Threading.Tasks;
using Feedback.Core.Entities;

namespace Feedback.Core.Services
{
    public interface IAuthenticationService
    {
        Task<User> LoginWithFacebookAsync();
    }
}