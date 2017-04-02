using Microsoft.WindowsAzure.MobileServices;

namespace Feedback.API.Services.Implementations
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