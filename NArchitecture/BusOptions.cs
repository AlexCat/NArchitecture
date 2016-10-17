using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Security;
using System;
using System.Collections.Generic;

namespace NArchitecture
{
    public class BusOptions
    {
        private readonly IList<Type> eventHandlers;
        private readonly IList<Type> requestHandlers;
        private readonly IList<Type> authorizationHandlers;

        public AuthorizationOptions AuthorizationOptions { get; }

        public BusOptions()
        {
            eventHandlers = new List<Type>();
            requestHandlers = new List<Type>();
            authorizationHandlers = new List<Type>();

            AuthorizationOptions = new AuthorizationOptions();
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

        public void AddAuthorizationHandler<TAuthorizationHandler>()
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            authorizationHandlers.Add(typeof(TAuthorizationHandler));
        }

        public void AddServicesTo(IServiceCollection services)
        {
            foreach(var eventHandler in eventHandlers)
            {
                services.AddTransient(typeof(IEventHandler), eventHandler);
            }

            foreach (var requestHandler in requestHandlers)
            {
                services.AddTransient(typeof(IRequestHandler), requestHandler);
            }

            foreach(var authorizationHandler in authorizationHandlers)
            {
                services.AddTransient(typeof(IAuthorizationHandler), authorizationHandler);
            }

            services.AddSingleton(AuthorizationOptions);
        }

        public void ConfigureAuthorization(Action<AuthorizationOptions> configure)
        {
            configure(AuthorizationOptions);
        }
    }
}
