namespace UnityApiPoc.Extension
{
    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;

    using Microsoft.Practices.Unity;

    public class UnityCompositionRoot : IHttpControllerActivator
    {
        private readonly IUnityContainer _container;

        public UnityCompositionRoot(IUnityContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (IHttpController)_container.Resolve(controllerType);

            request.RegisterForDispose(
                new Release(() => _container.Teardown(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                _release = release;
            }

            public void Dispose()
            {
                _release();
            }
        }
    }
}