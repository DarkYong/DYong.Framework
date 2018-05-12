using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DYong.Web.Startup))]
namespace DYong.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
