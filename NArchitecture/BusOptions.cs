using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Events;
using NArchitecture.Requests;
using NArchitecture.Security;
using System;

namespace NArchitecture
{
    public class BusOptions
    {
        public EventOptions EventOptions { get; }
        public RequestOptions RequestOptions { get; }
        public AuthorizationOptions AuthorizationOptions { get; }

        public BusOptions()
        {
            EventOptions = new EventOptions();
            RequestOptions = new RequestOptions();
            AuthorizationOptions = new AuthorizationOptions();
        }

        public void AddServicesTo(IServiceCollection services)
        {
            EventOptions.AddServicesTo(services);
            RequestOptions.AddServicesTo(services);
            AuthorizationOptions.AddServicesTo(services);
            services.AddSingleton(AuthorizationOptions);
        }

        public void ConfigureEvents(Action<EventOptions> configure)
        {
            configure(EventOptions);
        }

        public void ConfigureRequests(Action<RequestOptions> configure)
        {
            configure(RequestOptions);
        }

        public void ConfigureAuthorization(Action<AuthorizationOptions> configure)
        {
            configure(AuthorizationOptions);
        }
    }
}
