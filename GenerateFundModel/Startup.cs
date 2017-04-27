using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FundClear.Startup))]
namespace FundClear
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
