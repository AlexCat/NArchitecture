using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NArchitecture
{
    public class AuthorizationHandlerContext : BaseHandlerContext
    {
        private HashSet<IAuthorizationRequirement> pendingRequirements;
        private bool failCalled;
        private bool succeedCalled;

        public AuthorizationHandlerContext(IServiceBus bus, ClaimsPrincipal user, IMessage message, IEnumerable<IAuthorizationRequirement> requirements) : base(bus, user)
        {
            Guard.AgainstNull(nameof(message), message);
            Guard.AgainstNull(nameof(requirements), requirements);

            Message = message;
            Requirements = requirements;
            pendingRequirements = new HashSet<IAuthorizationRequirement>(requirements);
        }

        public virtual IEnumerable<IAuthorizationRequirement> Requirements { get; }
        public virtual IEnumerable<IAuthorizationRequirement> PendingRequirements { get { return pendingRequirements; } }
        public virtual IMessage Message { get; }

        public virtual bool HasSucceeded
        {
            get { return !failCalled && succeedCalled && !PendingRequirements.Any(); }
        }

        public virtual bool HasFailed
        {
            get { return failCalled; }
        }

        public virtual void Succeed(IAuthorizationRequirement requirement)
        {
            succeedCalled = true;
            pendingRequirements.Remove(requirement);
        }

        public virtual void Fail()
        {
            failCalled = true;
        }
    }
}
