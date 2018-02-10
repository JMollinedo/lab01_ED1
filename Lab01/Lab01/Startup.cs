using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab01.Startup))]
namespace Lab01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
