using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NArchitecture.Events;
using NArchitecture.Requests;
using NArchitecture.Security;
using NArchitecture.Validation;
using System;

namespace NArchitecture
{
    public static class BusServiceCollectionExtensions
    {
        public static IServiceCollection AddBus(this IServiceCollection services, Action<BusOptions> configure)
        {
            Guard.AgainstNull(nameof(services), services);
            Guard.AgainstNull(nameof(configure), configure);

            var busOptions = new BusOptions();
            configure(busOptions);
            busOptions.AddTo(services);

            services.TryAdd(ServiceDescriptor.Transient<IEventService, DefaultEventService>());
            services.TryAdd(ServiceDescriptor.Transient<IRequestService, DefaultRequestService>());
            services.TryAdd(ServiceDescriptor.Transient<IValidationService, DefaultValidationService>());
            services.TryAdd(ServiceDescriptor.Transient<IAuthorizationService, DefaultAuthorizationService>());
            services.TryAdd(ServiceDescriptor.Transient<IBus, Bus>());

            return services;
        }
    }
}
