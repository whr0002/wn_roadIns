using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(wn_RoadInspection.Startup))]
namespace wn_RoadInspection
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
