using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public class DefaultRequestService : IRequestService
    {
        private readonly IList<IRequestHandler> handlers;

        public DefaultRequestService(IEnumerable<IRequestHandler> handlers)
        {
            this.handlers = handlers.ToArray();
        }

        public Task Request(IServiceBus bus, ClaimsPrincipal user, IRequest request)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(user), user);
            Guard.AgainstNull(nameof(request), request);

            var handler = Find(request);
            var context = new RequestHandlerContext(bus, user);

            return handler.Handle(context, request);
        }

        public async Task<TResponse> Request<TResponse>(IServiceBus bus, ClaimsPrincipal user, IRequest<TResponse> request)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(user), user);
            Guard.AgainstNull(nameof(request), request);

            var handler = Find(request);
            var context = new RequestHandlerContext<TResponse>(bus, user);

            await handler.Handle(context, request);

            return context.Response == null ? default(TResponse) : context.Response;
        }

        private IRequestHandler Find(IRequest request)
        {
            try
            {
                return handlers.Single(h => h.CanHandle(request));
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException(Properties.Resources.NoneOrTooManyRequestHandlers, ex);
            }
        }
    }
}
