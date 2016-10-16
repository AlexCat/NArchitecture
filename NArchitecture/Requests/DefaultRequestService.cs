using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NArchitecture.Requests
{
    public class DefaultRequestService : IRequestService
    {
        private readonly IList<IRequestHandler> handlers;

        public DefaultRequestService(IEnumerable<IRequestHandler> handlers)
        {
            this.handlers = handlers.ToArray();
        }

        public Task Send(IBus bus, IRequest request)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(request), request);

            var handler = Find(request);
            var context = new RequestHandlerContext(bus, request);

            return handler.Handle(context);
        }

        public async Task<TResponse> Send<TResponse>(IBus bus, IRequest<TResponse> request)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(request), request);

            var handler = Find(request);
            var context = new RequestHandlerContext(bus, request);

            await handler.Handle(context);

            return (TResponse)context.Response;
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
