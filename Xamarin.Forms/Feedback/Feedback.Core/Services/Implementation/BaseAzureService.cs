using Microsoft.WindowsAzure.MobileServices;

namespace Feedback.Core.Services.Implementation
{
    public class BaseAzureService
    {
        static BaseAzureService()
        {
            MobileService = new MobileServiceClient(Constants.BaseApiUrl);
        }

        protected static MobileServiceClient MobileService { get; }
    }
}