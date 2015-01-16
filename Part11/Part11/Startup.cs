using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Part11.Startup))]
namespace Part11
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
