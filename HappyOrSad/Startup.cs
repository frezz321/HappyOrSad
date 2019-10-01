using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HappyOrSad.Startup))]
namespace HappyOrSad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
