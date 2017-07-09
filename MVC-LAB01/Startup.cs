using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_LAB01.Startup))]
namespace MVC_LAB01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
