using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Infinity_YAAB.Startup))]
namespace Infinity_YAAB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
