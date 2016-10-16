using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace NArchitecture
{
    public class BusOptions
    {
        private readonly IList<Type> eventHandlers;
        private readonly IList<Type> requestHandlers;

        public BusOptions()
        {
            eventHandlers = new List<Type>();
            requestHandlers = new List<Type>();
        }

        public void AddEventHandler<TEventHandler>()
            where TEventHandler : class, IEventHandler
        {
            eventHandlers.Add(typeof(TEventHandler));
        }

        public void AddRequestHandler<TRequestHandler>()
            where TRequestHandler : class, IRequestHandler
        {
            requestHandlers.Add(typeof(TRequestHandler));
        }

        public void AddTo(IServiceCollection services)
        {
            foreach(var eventHandler in eventHandlers)
            {
                services.AddTransient(typeof(IEventHandler), eventHandler);
            }

            foreach (var requestHandler in requestHandlers)
            {
                services.AddTransient(typeof(IRequestHandler), requestHandler);
            }
        }
    }
}
