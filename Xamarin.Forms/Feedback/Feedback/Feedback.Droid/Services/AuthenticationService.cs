using System;
using System.Threading.Tasks;
using Feedback.Core.Entities;
using Feedback.Core.Services.Implementation;
using Microsoft.WindowsAzure.MobileServices;

namespace Feedback.Droid.Services
{
    public class AuthenticationService : BaseAuthenticationService
    {
        public override async Task<User> LoginWithFacebookAsync()
        {
            if(MobileService == null)
                throw new NullReferenceException(nameof(MobileService));
            var result = await MobileService.LoginAsync(MainActivity.Instance, MobileServiceAuthenticationProvider.Facebook);
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