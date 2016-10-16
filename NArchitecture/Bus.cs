using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace NArchitecture
{
    public class Bus : IBus
    {
        private readonly IServiceProvider serviceProvider;

        public Bus(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [DebuggerStepThrough, DebuggerHidden]
        public virtual async Task Send(IEvent @event, CancellationToken cancellationToken)
        {
            Guard.AgainstNull(nameof(@event), @event);
            Validate(@event);
            var messageType = @event.GetType();
            var handlers = serviceProvider.GetEventHandlers(messageType, this);
            foreach (var handler in handlers)
            {
                await handler.Handle(@event, cancellationToken);
            }
        }

        [DebuggerStepThrough, DebuggerHidden]
        public virtual async Task Send(IRequest request, CancellationToken cancellationToken)
        {
            Guard.AgainstNull(nameof(request), request);
            Validate(request);
            var requestType = request.GetType();
            var handler = serviceProvider.GetRequestHandler(requestType, this);
            await handler.Handle(request, cancellationToken);
        }

        [DebuggerStepThrough, DebuggerHidden]
        public virtual async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            Guard.AgainstNull(nameof(request), request);
            Validate(request);
            var requestType = request.GetType();
            var handler = serviceProvider.GetRequestHandler<TResponse>(requestType, this);
            return await handler.Handle(request, cancellationToken);
        }

        protected virtual void Validate(IMessage message)
        {
            var validationContext = new ValidationContext(message);
            Validator.ValidateObject(message, validationContext);
        }
    }
}
