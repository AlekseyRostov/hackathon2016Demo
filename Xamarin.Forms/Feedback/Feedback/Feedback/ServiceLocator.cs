using Microsoft.Practices.Unity;

namespace Feedback.UI.Core
{
    public static class ServiceLocator
    {
        public static void Initialize()
        {
            Instance = new UnityContainer().RegisterUIDependencies();
        }

        public static IUnityContainer Instance { get; private set; }
    }
}