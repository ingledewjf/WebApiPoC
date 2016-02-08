namespace UnityApiPoc.Extension
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    public class UnityInterfaceInterceptionRegister : UnityContainerExtension
    {
        private List<Type> _interfaces = new List<Type>();

        private List<IInterceptionBehavior> _interceptionBehaviors = new List<IInterceptionBehavior>();

        public UnityInterfaceInterceptionRegister(Type interfaceType, IInterceptionBehavior behavior)
        {
            _interfaces.Add(interfaceType);
            _interceptionBehaviors.Add(behavior);
        }

        public UnityInterfaceInterceptionRegister(Type[] interfaces, IInterceptionBehavior[] behaviors)
        {
            _interfaces.AddRange(interfaces);
            _interceptionBehaviors.AddRange(behaviors);

            ValidateInterfaces(_interfaces);
        }

        protected override void Initialize()
        {
            Container.AddNewExtension<Interception>();

            Context.Registering += OnRegister;
        }

        private void ValidateInterfaces(List<Type> interfaces)
        {
            interfaces.ForEach(
                (i) =>
                    {
                        if (!i.IsInterface)
                        {
                            throw new ArgumentException("Only interfaces may be intercepted for interface interceptors.");
                        }
                    });
        }

        private bool ShouldIntercept(RegisterEventArgs e)
        {
            return e != null && e.TypeFrom != null && e.TypeFrom.IsInterface && _interfaces.Contains(e.TypeFrom);
        }

        private void OnRegister(object sender, RegisterEventArgs e)
        {
            if (ShouldIntercept(e))
            {
                var container = sender as IUnityContainer;

                var i = new Interceptor<InterfaceInterceptor>();
                i.AddPolicies(e.TypeFrom, e.TypeTo, e.Name, Context.Policies);

                _interceptionBehaviors.ForEach(
                    (b) =>
                        {
                            var interfaceBehavior = new InterceptionBehavior(b);
                            interfaceBehavior.AddPolicies(e.TypeFrom, e.TypeTo, e.Name, Context.Policies);
                        });
            }
        }
    }
}