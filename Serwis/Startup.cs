using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Serwis.Startup))]
namespace Serwis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
