namespace UnityApiPoc
{
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    using UnityApiPoc.Extension;

    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly IUnityContainer _container;

        public WebApiApplication()
        {
            _container = UnityInstaller.Install();
        }

        public void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new UnityCompositionRoot(_container));
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public override void Dispose()
        {
            _container.Dispose();
            base.Dispose();
        }
    }
}