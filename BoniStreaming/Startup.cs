using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoniStreaming.Startup))]
namespace BoniStreaming
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
