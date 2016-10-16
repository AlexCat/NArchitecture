using System.Linq;
using System.Threading.Tasks;

namespace NArchitecture.Security
{
    public abstract class AuthorizationHandler<TRequirement> : IAuthorizationHandler
        where TRequirement : IAuthorizationRequirement
    {
        public virtual async Task Handle(AuthorizationHandlerContext context)
        {
            foreach (var req in context.Requirements.OfType<TRequirement>())
            {
                await Handle(context, req);
            }
        }

        protected abstract Task Handle(AuthorizationHandlerContext context, TRequirement requirement);
    }

    public abstract class AuthorizationHandler<TRequirement, TMessage> : IAuthorizationHandler
        where TRequirement : IAuthorizationRequirement
    {
        public virtual async Task Handle(AuthorizationHandlerContext context)
        {
            if (context.Message is TMessage)
            {
                foreach (var req in context.Requirements.OfType<TRequirement>())
                {
                    await Handle(context, req, (TMessage)context.Message);
                }
            }
        }

        protected abstract Task Handle(AuthorizationHandlerContext context, TRequirement requirement, TMessage resource);
    }
}
