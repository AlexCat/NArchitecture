using DryIoc;
using System;

namespace NArchitecture.Tests
{
    internal class UnitTestServiceProvider : IServiceProvider, IDisposable
    {
        private readonly Container container;

        public UnitTestServiceProvider()
        {
            container = new Container();
        }

        public void RegisterHandler<THandler>()
            where THandler : IHandle
        {
            Type[] serviceTypes = typeof(THandler).GetHandlerServiceTypes();
            foreach(var serviceType in serviceTypes)
            {
                container.Register(serviceType, typeof(THandler));
            }
        }

        public object GetService(Type type)
        {
            return container.Resolve(type);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }

}
