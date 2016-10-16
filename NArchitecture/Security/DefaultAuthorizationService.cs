using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture.Security
{
    public class DefaultAuthorizationService : IAuthorizationService
    {
        private readonly AuthorizationOptions options;
        private readonly IList<IAuthorizationHandler> handlers;

        public DefaultAuthorizationService(AuthorizationOptions options, IEnumerable<IAuthorizationHandler> handlers)
        {
            this.options = options;
            this.handlers = handlers.ToArray();
        }

        public async Task<bool> Authorize(ClaimsPrincipal user, IMessage message, IEnumerable<IAuthorizationRequirement> requirements)
        {
            Guard.AgainstNull(nameof(requirements), requirements);

            var context = new AuthorizationHandlerContext(requirements, user, message);

            foreach(var handler in handlers)
            {
                await handler.Handle(context);
            }

            return context.HasSucceeded;
        }

        public Task<bool> Authorize(ClaimsPrincipal user, IMessage message, string policyName)
        {
            Guard.AgainstNull(nameof(policyName), policyName);

            var policy = options.GetPolicy(policyName);
            if (policy == null)
            {
                throw new InvalidOperationException($"No policy found: {policyName}.");
            }

            return Authorize(user, message, policy.Requirements);
        }
    }
}
