using Feedback.Core.Services;
using Feedback.Core.Services.Implementation;
using Microsoft.Practices.Unity;

namespace Feedback.Core
{
    public static class CoreBootstrapper
    {
        public static IUnityContainer RegisterCoreDependencies(this IUnityContainer unityContainer)
        {
            return unityContainer.RegisterType<IFeedbackService, FeedbackService>()
                                 .RegisterType<IPlaceService, PlaceService>();
        }
    }
}