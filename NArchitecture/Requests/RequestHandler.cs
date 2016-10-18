using System.Threading.Tasks;

namespace NArchitecture.Requests
{
    public abstract class RequestHandler<TRequest> : IRequestHandler
        where TRequest : IRequest
    {
        public virtual bool CanHandle(IRequest request)
        {
            return request is TRequest;
        }

        public virtual Task Handle(RequestHandlerContext context)
        {
            var requestContext = (RequestHandlerContext<TRequest>)context;
            return Handle(requestContext);
        }

        protected abstract Task Handle(RequestHandlerContext<TRequest> context);
    }

    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler
        where TRequest : IRequest<TResponse>
    {
        public bool CanHandle(IRequest request)
        {
            return request is TRequest;
        }

        public virtual async Task Handle(RequestHandlerContext context)
        {
            var requestContext = (RequestHandlerContext<TRequest, TResponse>)context;
            await Handle(requestContext);
        }

        protected abstract Task Handle(RequestHandlerContext<TRequest, TResponse> context);
    }
}
