using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SFA.Startup))]
namespace SFA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
