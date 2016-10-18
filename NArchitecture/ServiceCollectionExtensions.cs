using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NArchitecture.Events;
using NArchitecture.Requests;
using NArchitecture.Security;
using NArchitecture.Validation;
using System;

namespace NArchitecture
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBus(
            this IServiceCollection services, Action<BusOptions> configure)
        {
            Guard.AgainstNull(nameof(services), services);
            Guard.AgainstNull(nameof(configure), configure);

            var options = new BusOptions();
            configure(options);
            return AddBus(services, options);
        }

        public static IServiceCollection AddEventService(
            this IServiceCollection services, Action<EventComposition> configure)
        {
            Guard.AgainstNull(nameof(services), services);
            Guard.AgainstNull(nameof(configure), configure);

            var options = new EventComposition();
            configure(options);
            return AddEventService(services, options);
        }

        public static IServiceCollection AddRequestService(
            this IServiceCollection services, Action<RequestComposition> configure)
        {
            Guard.AgainstNull(nameof(services), services);
            Guard.AgainstNull(nameof(configure), configure);

            var options = new RequestComposition();
            configure(options);
            return AddRequestService(services, options);
        }

        public static IServiceCollection AddAuthorizationService(
            this IServiceCollection services, Action<AuthorizationComposition> configure)
        {
            Guard.AgainstNull(nameof(services), services);
            Guard.AgainstNull(nameof(configure), configure);

            var options = new AuthorizationComposition();
            configure(options);
            return AddAuthorizationService(services, options);
        }

        public static IServiceCollection AddValidationService(
            this IServiceCollection services)
        {
            Guard.AgainstNull(nameof(services), services);

            services.TryAdd(ServiceDescriptor.Transient<IValidationService, DefaultValidationService>());
            return services;
        }

        private static IServiceCollection AddBus(
            this IServiceCollection services, BusOptions options)
        {
            services = AddEventService(services, options.Events);
            services = AddRequestService(services, options.Requests);
            services = AddAuthorizationService(services, options.Authorization);
            services = AddValidationService(services);
            services.AddSingleton(options);
            services.TryAdd(ServiceDescriptor.Transient<IBus, Bus>());
            return services;
        }

        private static IServiceCollection AddEventService(
            this IServiceCollection services, EventComposition composition)
        {
            composition.AddServicesTo(services);
            services.TryAdd(ServiceDescriptor.Transient<IEventService, DefaultEventService>());
            return services;
        }

        private static IServiceCollection AddRequestService(
            this IServiceCollection services, RequestComposition composition)
        {
            composition.AddServicesTo(services);
            services.TryAdd(ServiceDescriptor.Transient<IRequestService, DefaultRequestService>());
            return services;
        }

        private static IServiceCollection AddAuthorizationService(
            this IServiceCollection services, AuthorizationComposition composition)
        {
            composition.AddServicesTo(services);
            services.AddSingleton(composition.Options);
            services.TryAdd(ServiceDescriptor.Transient<IAuthorizationService, DefaultAuthorizationService>());
            return services;
        }
    }
}
