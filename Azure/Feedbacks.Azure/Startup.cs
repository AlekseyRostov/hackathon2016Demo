using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Feedbacks.Azure.Startup))]

namespace Feedbacks.Azure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}