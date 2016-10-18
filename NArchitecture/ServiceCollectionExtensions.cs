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
            this IServiceCollection services, Action<EventOptions> configure)
        {
            Guard.AgainstNull(nameof(services), services);
            Guard.AgainstNull(nameof(configure), configure);

            var options = new EventOptions();
            configure(options);
            return AddEventService(services, options);
        }

        public static IServiceCollection AddRequestService(
            this IServiceCollection services, Action<RequestOptions> configure)
        {
            Guard.AgainstNull(nameof(services), services);
            Guard.AgainstNull(nameof(configure), configure);

            var options = new RequestOptions();
            configure(options);
            return AddRequestService(services, options);
        }

        public static IServiceCollection AddAuthorizationService(
            this IServiceCollection services, Action<AuthorizationOptions> configure)
        {
            Guard.AgainstNull(nameof(services), services);
            Guard.AgainstNull(nameof(configure), configure);

            var options = new AuthorizationOptions();
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
            services = AddEventService(services, options.EventOptions);
            services = AddRequestService(services, options.RequestOptions);
            services = AddAuthorizationService(services, options.AuthorizationOptions);
            services = AddValidationService(services);
            services.TryAdd(ServiceDescriptor.Transient<IBus, Bus>());
            return services;
        }

        private static IServiceCollection AddEventService(
            this IServiceCollection services, EventOptions options)
        {
            options.AddServicesTo(services);
            services.TryAdd(ServiceDescriptor.Transient<IEventService, DefaultEventService>());
            return services;
        }

        private static IServiceCollection AddRequestService(
            this IServiceCollection services, RequestOptions options)
        {
            options.AddServicesTo(services);
            services.TryAdd(ServiceDescriptor.Transient<IRequestService, DefaultRequestService>());
            return services;
        }

        private static IServiceCollection AddAuthorizationService(
            this IServiceCollection services, AuthorizationOptions options)
        {
            options.AddServicesTo(services);
            services.TryAdd(ServiceDescriptor.Transient<IAuthorizationService, DefaultAuthorizationService>());
            return services;
        }
    }
}
