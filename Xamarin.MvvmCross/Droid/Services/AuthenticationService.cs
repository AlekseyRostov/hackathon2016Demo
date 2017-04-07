using System.Threading.Tasks;
using Android.Webkit;
using Feedback.Core.Services.Implementation;
using Microsoft.WindowsAzure.MobileServices;
using Android.App;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace Feedback.Droid.Services
{
    public class AuthenticationService : BaseAuthenticationService
    {
        protected override Task<MobileServiceUser> GetFacebookUserAsync()
        {
            return MobileService.LoginAsync(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity, MobileServiceAuthenticationProvider.Facebook);
        }

        protected override void LogoutNative()
        {
            CookieManager.Instance.RemoveAllCookie();
        }
    }
}