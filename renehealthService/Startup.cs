using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(renehealthService.Startup))]

namespace renehealthService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}