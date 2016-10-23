using System;
using System.Collections.Generic;
using System.Linq;

namespace NArchitecture
{
    public class AuthorizationPolicy
    {
        public AuthorizationPolicy(IEnumerable<IAuthorizationRequirement> requirements)
        {
            Guard.AgainstNull(nameof(requirements), requirements);
            if (requirements.Count() == 0)
            {
                throw new InvalidOperationException(Properties.Resources.PolicyHasNoRequirements);
            }

            Requirements = new List<IAuthorizationRequirement>(requirements).AsReadOnly();
        }

        public IReadOnlyList<IAuthorizationRequirement> Requirements { get; }

        public static AuthorizationPolicy Combine(params AuthorizationPolicy[] policies)
        {
            Guard.AgainstNull(nameof(policies), policies);

            return Combine((IEnumerable<AuthorizationPolicy>)policies);
        }

        public static AuthorizationPolicy Combine(IEnumerable<AuthorizationPolicy> policies)
        {
            Guard.AgainstNull(nameof(policies), policies);

            var builder = new AuthorizationPolicyBuilder();
            foreach (var policy in policies)
            {
                builder.Combine(policy);
            }
            return builder.Build();
        }
    }
}
