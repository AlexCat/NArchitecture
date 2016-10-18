using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace NArchitecture.Security
{
    public class AuthorizationOptions : BaseOptions
    {
        private IDictionary<string, AuthorizationPolicy> PolicyMap { get; } = new Dictionary<string, AuthorizationPolicy>(StringComparer.OrdinalIgnoreCase);

        public AuthorizationOptions() : base(typeof(IAuthorizationHandler)) { }

        public void AddAuthorizationHandler<TAuthorizationHandler>()
            where TAuthorizationHandler : class, IAuthorizationHandler
        {
            handlers.Add(typeof(TAuthorizationHandler));
        }

        public void AddPolicy(string name, AuthorizationPolicy policy)
        {
            Guard.AgainstNull(nameof(name), name);
            Guard.AgainstNull(nameof(policy), policy);

            PolicyMap[name] = policy;
        }

        public AuthorizationPolicy GetPolicy(string name)
        {
            Guard.AgainstNull(nameof(name), name);

            return PolicyMap.ContainsKey(name) ? PolicyMap[name] : null;
        }

        public override void AddServicesTo(IServiceCollection services)
        {
            base.AddServicesTo(services);
            services.AddSingleton(this);
        }
    }
}
