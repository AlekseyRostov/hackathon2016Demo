using Feedback.Core.Services;
using Feedback.Core.Services.Cognitive;
using Feedback.Core.Services.Implementation;
using Feedback.Core.Services.Implementation.Cognitive;
using Microsoft.Practices.Unity;

namespace Feedback.Core
{
    public static class CoreBootstrapper
    {
        public static IUnityContainer RegisterCoreDependencies(this IUnityContainer unityContainer)
        {
            return unityContainer.RegisterType<IFeedbackService, FeedbackService>()
                                 .RegisterType<IPlaceService, PlaceService>()
                                 .RegisterType<ICognitiveServiceAuthenticator, CognitiveServiceAuthenticator>()
                                 .RegisterType<ISpeechService, BingSpeechService>();
        }
    }
}