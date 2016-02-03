namespace UnityApiPoc.Extension
{
    using Microsoft.Practices.Unity;

    using UnityApiPoc.Helpers;
    using UnityApiPoc.Values;

    public static class UnityInstaller
    {
        public static UnityContainer Install()
        {
            var container = new UnityContainer();

            container.RegisterType(typeof(IValuesProvider), typeof(ValuesProvider), new PerRequestLifetimeManager());
            container.RegisterType(
                typeof(IDisposableValuesProvider),
                typeof(DisposableValuesProvider),
                new PerRequestLifetimeManager());
            container.RegisterType(typeof(System.Web.Http.Tracing.ITraceWriter), typeof(NLogger), new HierarchicalLifetimeManager());

            return container;
        }
    }
}