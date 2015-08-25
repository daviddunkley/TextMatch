using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TextMatch.Startup))]
namespace TextMatch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
