using Feedback.Core.Services;
using Feedback.UI.iOS.Services;
using Feedback.UI.Services;
using Microsoft.Practices.Unity;

namespace Feedback.UI.iOS
{
    public static class iOSBootstrapper
    {
        public static IUnityContainer RegisteriOSDependencies(this IUnityContainer container)
        {
            return container.RegisterType<IAuthenticationService, AuthenticationService>(new ContainerControlledLifetimeManager())
                            .RegisterType<IAudioRecorderService, AudioRecorderService>()
                            .RegisterType<IDeviceService, DeviceService>()
                            .RegisterType<IBeaconLocationService, BeaconLocationService>();
            ;
        }
    }
}