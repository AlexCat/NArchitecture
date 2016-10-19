using System.Collections.Generic;
using System.Linq;

namespace NArchitecture
{
    public class AuthorizationPolicyBuilder
    {
        public IList<IAuthorizationRequirement> Requirements { get; set; } = new List<IAuthorizationRequirement>();

        public AuthorizationPolicyBuilder AddRequirements(params IAuthorizationRequirement[] requirements)
        {
            foreach (var req in requirements)
            {
                Requirements.Add(req);
            }

            return this;
        }

        public AuthorizationPolicyBuilder Combine(AuthorizationPolicy policy)
        {
            Guard.AgainstNull(nameof(policy), policy);

            AddRequirements(policy.Requirements.ToArray());
            return this;
        }

        public AuthorizationPolicy Build()
        {
            return new AuthorizationPolicy(Requirements);
        }
    }
}
