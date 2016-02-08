namespace UnityApiPoc.Extension
{
    using System;
    using System.CodeDom;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    using UnityApiPoc.Helpers;
    using UnityApiPoc.Values;

    public static class UnityInstaller
    {
        public static UnityContainer Install()
        {
            var container = new UnityContainer();

            container.AddExtension(
                new UnityInterfaceInterceptionRegister(
                    new[] { typeof(IValuesProvider), typeof(IDisposableValuesProvider) },
                    new IInterceptionBehavior[] { new LoggingInterceptionBehaviour() }));

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