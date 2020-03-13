using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(realMiniProjet.Startup))]
namespace realMiniProjet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
