using System;
using System.Collections.Generic;

namespace NArchitecture.Security
{
    public class AuthorizationOptions
    {
        private IDictionary<string, AuthorizationPolicy> PolicyMap { get; } = new Dictionary<string, AuthorizationPolicy>(StringComparer.OrdinalIgnoreCase);

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
    }
}
