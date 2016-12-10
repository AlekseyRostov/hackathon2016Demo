using System.Threading.Tasks;
using Feedback.Core.Entities;

namespace Feedback.Core.Services.Implementation
{
    public abstract class BaseAuthenticationService : BaseAzureService, IAuthenticationService
    {
        public abstract Task<User> LoginWithFacebookAsync();
    }
}