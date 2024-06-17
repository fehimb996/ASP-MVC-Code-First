using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RVASIspit.Startup))]
namespace RVASIspit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
