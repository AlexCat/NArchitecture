using System;
using System.Collections.Generic;
using System.Linq;

namespace NArchitecture.Security
{
    public class AuthorizationPolicy
    {
        public AuthorizationPolicy(IEnumerable<IAuthorizationRequirement> requirements)
        {
            Guard.AgainstNull(nameof(requirements), requirements);
            if (requirements.Count() == 0)
            {
                throw new InvalidOperationException("Policy needs at least one authorization requirement.");
            }

            Requirements = new List<IAuthorizationRequirement>(requirements).AsReadOnly();
        }

        public IReadOnlyList<IAuthorizationRequirement> Requirements { get; }
    }
}
