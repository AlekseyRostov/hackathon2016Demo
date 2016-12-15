using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Feedback.Core.Entities;
using Microsoft.WindowsAzure.MobileServices;
using PubSub;

namespace Feedback.Core.Services.Implementation
{
    public abstract class BaseAuthenticationService : BaseAzureService, IAuthenticationService
    {
        private readonly string _userCacheKey = $"{Constants.BundleId}:CurrentUser";
        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            private set
            {
                _currentUser = value;
                FireCurrentUserChanged();
            }
        }

        private void FireCurrentUserChanged()
        {
            this.Publish(CurrentUser);
        }

        public async Task LoginWithFacebookAsync()
        {
            if(MobileService == null)
                throw new NullReferenceException(nameof(MobileService));
            var result = await GetFacebookUserAsync();
            if(result != null)
            {
                CurrentUser = new User
                              {
                                  Id = result.UserId,
                                  Token = result.MobileServiceAuthenticationToken
                              };
                await StoreCurrentUser();
            }
            else
            {
                CurrentUser = null;
            }
        }

        protected abstract Task<MobileServiceUser> GetFacebookUserAsync();

        public void RestoreSession()
        {
            User user = null;
            try
            {
                user = BlobCache.LocalMachine.GetObject<User>(_userCacheKey).Wait();
                MobileService.CurrentUser = new MobileServiceUser(user.Id)
                                            {
                                                MobileServiceAuthenticationToken = user.Token
                                            };
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                CurrentUser = user;
            }
        }

        public async Task LogoutAsync()
        {
            await DeleteCurrentUser();
            LogoutNative();
            await MobileService.LogoutAsync();
            CurrentUser = null;
        }

        protected abstract void LogoutNative();

        private async Task DeleteCurrentUser()
        {
            try
            {
                await BlobCache.LocalMachine.Invalidate(_userCacheKey);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task StoreCurrentUser()
        {
            await BlobCache.LocalMachine.InsertObject(_userCacheKey, CurrentUser);
        }
    }
}