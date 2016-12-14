using Feedback.Core.Services;
using Feedback.UI.Droid.Services;
using Feedback.UI.Services;
using Microsoft.Practices.Unity;

namespace Feedback.UI.Droid
{
    public static class DroidBootstrapper
    {
        public static IUnityContainer RegisterDroidDependencies(this IUnityContainer container)
        {
            return container.RegisterType<IAuthenticationService, AuthenticationService>(new ContainerControlledLifetimeManager())
                            .RegisterType<IAudioRecorderService, AudioRecorderService>()
                            .RegisterType<IDeviceService, DeviceService>();
        }
    }
}