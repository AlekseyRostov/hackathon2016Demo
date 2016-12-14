using Feedback.Core.Services;
using Feedback.iOS.Services;
using Feedback.UI.Services;
using Microsoft.Practices.Unity;

namespace Feedback.iOS
{
    public static class iOSBootstrapper
    {
        public static IUnityContainer RegisteriOSDependencies(this IUnityContainer container)
        {
            return container.RegisterType<IAuthenticationService, AuthenticationService>(new ContainerControlledLifetimeManager())
                            .RegisterType<IAudioRecorderService, AudioRecorderService>();
            ;
        }
    }
}