using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ResortPlay.Startup))]
namespace ResortPlay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
