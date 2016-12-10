using Microsoft.Practices.Unity;

namespace Feedback.UI.Core
{
    public static class ServiceLocator
    {
        static ServiceLocator()
        {
            Instance = new UnityContainer().RegisterUIDependencies();
        }

        public static IUnityContainer Instance { get; }
    }
}