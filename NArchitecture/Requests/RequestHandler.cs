using System;
using System.Threading.Tasks;

namespace NArchitecture
{
    public abstract class RequestHandler<TRequest> : IRequestHandler
        where TRequest : IRequest
    {
        public virtual bool CanHandle(IRequest request)
        {
            return request is TRequest;
        }

        public virtual Task Handle(RequestHandlerContext context, IRequest request)
        {
            if (!CanHandle(request))
            {
                string message = string.Format(Properties.Resources.CannotHandleRequest, request.GetType().Name);
                throw new ArgumentException(message, nameof(request));
            }

            return Handle(context, (TRequest)request);
        }

        protected abstract Task Handle(RequestHandlerContext context, TRequest request);
    }

    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler
        where TRequest : IRequest<TResponse>
    {
        public bool CanHandle(IRequest request)
        {
            return request is TRequest;
        }

        public virtual async Task Handle(RequestHandlerContext context, IRequest request)
        {
            if (!CanHandle(request))
            {
                string message = string.Format(Properties.Resources.CannotHandleRequest, request.GetType().Name);
                throw new ArgumentException(message, nameof(request));
            }

            await Handle((RequestHandlerContext<TResponse>)context, (TRequest)request);
        }

        protected abstract Task Handle(RequestHandlerContext<TResponse> context, TRequest request);
    }
}
