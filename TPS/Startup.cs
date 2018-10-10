using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TPS.Startup))]
namespace TPS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
