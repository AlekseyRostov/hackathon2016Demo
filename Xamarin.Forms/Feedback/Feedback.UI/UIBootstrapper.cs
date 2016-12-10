using Feedback.Core;
using Feedback.UI.ViewModels.Authentication;
using Feedback.UI.ViewModels.Authentication.Implementation;
using Feedback.UI.ViewModels.Feedbacks;
using Feedback.UI.ViewModels.Feedbacks.Implementation;
using Feedback.UI.ViewModels.Places;
using Feedback.UI.ViewModels.Places.Implementation;
using Microsoft.Practices.Unity;

namespace Feedback.UI
{
    public static class UIBootstrapper
    {
        public static IUnityContainer RegisterUIDependencies(this IUnityContainer unityContainer)
        {
            return unityContainer.RegisterType<PlacesFactory>()
                                 .RegisterType<IPlacesViewModel, PlacesViewModel>()
                                 .RegisterType<FeedbacksFactory>()
                                 .RegisterType<IFeedbacksViewModel, FeedbacksViewModel>()
                                 .RegisterType<AuthenticationFactory>()
                                 .RegisterType<ILoginViewModel, LoginViewModel>()
                                 .RegisterCoreDependencies();
        }
    }
}