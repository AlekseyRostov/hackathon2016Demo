using Microsoft.WindowsAzure.MobileServices;

namespace Feedback.Core.Services.Implementation
{
    public class BaseAzureService
    {
        public BaseAzureService()
        {
            MobileService = new MobileServiceClient(Constants.BaseApiUrl);
        }

        protected MobileServiceClient MobileService { get; }
    }
}