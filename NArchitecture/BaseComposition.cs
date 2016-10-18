using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace NArchitecture
{
    public class BaseComposition
    {
        protected Type serviceType;
        protected readonly IList<Type> handlers;

        public BaseComposition(Type serviceType)
        {
            this.serviceType = serviceType;
            handlers = new List<Type>();
        }

        public virtual void AddServicesTo(IServiceCollection services)
        {
            foreach(var handler in handlers)
            {
                services.AddTransient(serviceType, handler);
            }
        }
    }
}
