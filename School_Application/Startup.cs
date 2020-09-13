using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(School_Application.Startup))]
namespace School_Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
