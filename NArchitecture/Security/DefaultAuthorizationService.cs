using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
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

        public async Task<bool> Authorize(IServiceBus bus, ClaimsPrincipal user, IMessage message, IEnumerable<IAuthorizationRequirement> requirements)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(user), user);
            Guard.AgainstNull(nameof(message), message);
            Guard.AgainstNull(nameof(requirements), requirements);

            var context = new AuthorizationHandlerContext(bus, user, message, requirements);

            foreach(var handler in handlers)
            {
                await handler.Handle(context);
            }

            return context.HasSucceeded;
        }

        public Task<bool> Authorize(IServiceBus bus, ClaimsPrincipal user, IMessage message, string policyName)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(user), user);
            Guard.AgainstNull(nameof(message), message);
            Guard.AgainstNull(nameof(policyName), policyName);

            var policy = options.GetPolicy(policyName);
            if (policy == null)
            {
                string exMessage = string.Format(Properties.Resources.PolicyNotFound, policyName);
                throw new InvalidOperationException(exMessage);
            }

            return Authorize(bus, user, message, policy.Requirements);
        }
    }
}
