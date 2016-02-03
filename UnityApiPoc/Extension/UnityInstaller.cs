namespace UnityApiPoc.Extension
{
    using Microsoft.Practices.Unity;

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

            return container;
        }
    }
}