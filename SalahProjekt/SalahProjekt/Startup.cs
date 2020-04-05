using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalahProjekt.Startup))]
namespace SalahProjekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
