namespace UnityApiPoc
{
    using System.Web.Http;

    using Microsoft.Owin.Security.OAuth;

    using UnityApiPoc.ActionFilters;
    using UnityApiPoc.Helpers;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Filters.Add(new LoggingFilterAttribute());
            config.Filters.Add(new GlobalExceptionAttribute());
            config.Services.Replace(typeof(System.Web.Http.Tracing.ITraceWriter), new NLogger());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}
