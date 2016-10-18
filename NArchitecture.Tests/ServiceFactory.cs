using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Events;
using NArchitecture.Requests;
using NArchitecture.Security;
using System;

namespace NArchitecture.Tests
{
    public static class ServiceFactory
    {
        public static IEventService CreateEventService(Action<EventOptions> configure)
        {
            var services = new ServiceCollection();
            services.AddEventService(configure);
            var provider = services.BuildServiceProvider();
            return provider.GetService<IEventService>();
        }

        public static IRequestService CreateRequestService(Action<RequestOptions> configure)
        {
            var services = new ServiceCollection();
            services.AddRequestService(configure);
            var provider = services.BuildServiceProvider();
            return provider.GetService<IRequestService>();
        }

        public static IAuthorizationService CreateAuthorizationService(Action<AuthorizationOptions> configure)
        {
            var services = new ServiceCollection();
            services.AddAuthorizationService(configure);
            var provider = services.BuildServiceProvider();
            return provider.GetService<IAuthorizationService>();
        }

        public static IValidationService CreateValidationService()
        {
            var services = new ServiceCollection();
            services.AddValidationService();
            var provider = services.BuildServiceProvider();
            return provider.GetService<IValidationService>();
        }
    }
}
