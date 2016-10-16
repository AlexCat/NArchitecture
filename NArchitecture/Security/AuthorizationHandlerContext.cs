using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NArchitecture.Security
{
    public class AuthorizationHandlerContext
    {
        private HashSet<IAuthorizationRequirement> pendingRequirements;
        private bool failCalled;
        private bool succeedCalled;

        public AuthorizationHandlerContext(IEnumerable<IAuthorizationRequirement> requirements, ClaimsPrincipal user, IMessage message)
        {
            Guard.AgainstNull(nameof(requirements), requirements);

            Requirements = requirements;
            User = user;
            Message = message;
            pendingRequirements = new HashSet<IAuthorizationRequirement>(requirements);
        }

        public virtual IEnumerable<IAuthorizationRequirement> Requirements { get; }
        public virtual IEnumerable<IAuthorizationRequirement> PendingRequirements { get { return pendingRequirements; } }
        public virtual ClaimsPrincipal User { get; }
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
