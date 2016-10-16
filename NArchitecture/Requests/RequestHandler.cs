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
            var request = (TRequest)context.Request;
            return Handle(request);
        }

        protected abstract Task Handle(TRequest request);
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
            var request = (TRequest)context.Request;
            context.Respond(await Handle(request));
        }

        protected abstract Task<TResponse> Handle(TRequest request);
    }
}
