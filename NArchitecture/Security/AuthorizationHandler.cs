﻿using System.Linq;
using System.Threading.Tasks;

namespace NArchitecture
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
}
