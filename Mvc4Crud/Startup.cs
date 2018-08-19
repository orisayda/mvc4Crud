using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc4Crud.Startup))]
namespace Mvc4Crud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
