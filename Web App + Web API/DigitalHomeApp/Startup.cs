using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitalHomeApp.Startup))]
namespace DigitalHomeApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
