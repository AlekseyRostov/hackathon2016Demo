using Feedback.Core.Services;
using Feedback.Droid.Services;
using Microsoft.Practices.Unity;

namespace Feedback.Droid
{
    public static class DroidBootstrapper
    {
        public static IUnityContainer RegisterDroidDependencies(this IUnityContainer container)
        {
            return container.RegisterType<IAuthenticationService, AuthenticationService>();
        }
    }
}