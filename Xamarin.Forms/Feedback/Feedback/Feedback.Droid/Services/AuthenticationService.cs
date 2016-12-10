using System.Threading.Tasks;
using Feedback.Core.Services.Implementation;
using Microsoft.WindowsAzure.MobileServices;

namespace Feedback.Droid.Services
{
    public class AuthenticationService : BaseAuthenticationService
    {
        protected override Task<MobileServiceUser> GetFacebookUserAsync()
        {
            return MobileService.LoginAsync(MainActivity.Instance, MobileServiceAuthenticationProvider.Facebook);
        }
    }
}