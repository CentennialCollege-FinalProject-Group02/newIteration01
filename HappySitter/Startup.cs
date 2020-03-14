using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HappySitter.Startup))]
namespace HappySitter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
