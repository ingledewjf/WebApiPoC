using Microsoft.Owin;

[assembly: OwinStartup(typeof(UnityApiPoc.Startup))]

namespace UnityApiPoc
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}