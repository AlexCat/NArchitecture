using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task Request(IServiceBus bus, IRequest request)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(request), request);

            var handler = Find(request);
            var context = new RequestHandlerContext(bus);

            return handler.Handle(context, request);
        }

        public async Task<TResponse> Request<TResponse>(IServiceBus bus, IRequest<TResponse> request)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(request), request);

            var handler = Find(request);
            var context = new RequestHandlerContext<TResponse>(bus);

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
                throw new InvalidOperationException("There is no or too many handlers to handle the request.", ex);
            }
        }
    }
}
