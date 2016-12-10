using System.Threading.Tasks;
using Feedback.Core.Services.Implementation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace Feedback.iOS.Services
{
    public class AuthenticationService : BaseAuthenticationService
    {
        protected override Task<MobileServiceUser> GetFacebookUserAsync()
        {
            return MobileService.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, MobileServiceAuthenticationProvider.Facebook);
        }
    }
}