using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5Week1.Startup))]
namespace MVC5Week1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
