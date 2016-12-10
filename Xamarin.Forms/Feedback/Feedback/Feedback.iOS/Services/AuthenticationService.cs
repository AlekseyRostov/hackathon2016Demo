using System;
using System.Threading.Tasks;
using Feedback.Core.Entities;
using Feedback.Core.Services.Implementation;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;

namespace Feedback.iOS.Services
{
    public class AuthenticationService : BaseAuthenticationService
    {
        public override async Task<User> LoginWithFacebookAsync()
        {
            if(MobileService == null)
                throw new NullReferenceException(nameof(MobileService));
            var result = await MobileService.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, MobileServiceAuthenticationProvider.Facebook);
            User user = null;
            if(result != null)
            {
                user = new User
                       {
                           Id = result.UserId,
                           Token = result.MobileServiceAuthenticationToken
                       };
            }
            return user;
        }
    }
}