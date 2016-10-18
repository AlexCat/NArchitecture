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

        public Task Request(IBus bus, IRequest request)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(request), request);

            var handler = Find(request);
            var context = CreateRequestHandlerContext(bus, request);

            return handler.Handle(context);
        }

        public async Task<TResponse> Request<TResponse>(IBus bus, IRequest<TResponse> request)
        {
            Guard.AgainstNull(nameof(bus), bus);
            Guard.AgainstNull(nameof(request), request);

            var handler = Find(request);
            var context = CreateRequestHandlerContext(bus, request);

            await handler.Handle(context);

            return context.Response == null ? default(TResponse) : (TResponse)context.Response;
        }

        private RequestHandlerContext CreateRequestHandlerContext(IBus bus, IRequest request)
        {
            Type contextType = typeof(RequestHandlerContext<>).MakeGenericType(request.GetType());
            return (RequestHandlerContext)Activator.CreateInstance(contextType, bus, request);
        }

        private RequestHandlerContext CreateRequestHandlerContext<TResponse>(IBus bus, IRequest<TResponse> request)
        {
            Type contextType = typeof(RequestHandlerContext<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            return (RequestHandlerContext)Activator.CreateInstance(contextType, bus, request);
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
