using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JLFinancialApp.Startup))]
namespace JLFinancialApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
