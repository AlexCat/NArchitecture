using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace NArchitecture
{
    public static class IServiceProviderExtensions
    {
        private const string HandlerNotFound =
            "Handler was not found for request of type ";

        public static IEnumerable<object> GetServices(this IServiceProvider serviceProvider, Type serviceType)
        {
            Guard.AgainstNull(nameof(serviceProvider), serviceProvider);
            Guard.AgainstNull(nameof(serviceType), serviceType);

            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return (IEnumerable<object>)serviceProvider.GetService(enumerableType);
        }

        private static void AddBus(object handler, IBus bus)
        {
            if (typeof(MessageHandler).IsAssignableFrom(handler.GetType()))
            {
                var messageHandler = (MessageHandler)handler;
                messageHandler.Bus = bus;
            }
        }

        #region Events
        [DebuggerStepThrough, DebuggerHidden]
        internal static IEnumerable<EventHandler> GetEventHandlers(this IServiceProvider serviceProvider, Type eventType, IBus bus = null)
        {
            var serviceType = typeof(IHandleEvent<>).MakeGenericType(eventType);
            var handlers = serviceProvider.GetServices(serviceType);
            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    AddBus(handler, bus);
                    yield return WrapEventHandler(eventType, handler);
                }
            }
        }

        private static EventHandler WrapEventHandler(Type eventType, object handler)
        {
            var wrapperType = typeof(EventHandler<>).MakeGenericType(eventType);
            return (EventHandler)Activator.CreateInstance(wrapperType, handler);
        }

        internal abstract class EventHandler
        {
            public abstract Task Handle(IEvent @event, CancellationToken cancellationToken);
        }

        internal sealed class EventHandler<TEvent> : EventHandler
            where TEvent : IEvent
        {
            private readonly IHandleEvent<TEvent> _inner;

            public EventHandler(IHandleEvent<TEvent> inner)
            {
                _inner = inner;
            }

            [DebuggerStepThrough, DebuggerHidden]
            public override async Task Handle(IEvent @event, CancellationToken cancellationToken)
            {
                await _inner.Handle((TEvent)@event, cancellationToken);
            }
        }
        #endregion

        #region Requests
        [DebuggerStepThrough, DebuggerHidden]
        internal static RequestHandler GetRequestHandler(this IServiceProvider serviceProvider, Type requestType, IBus bus = null)
        {
            var serviceType = typeof(IHandleRequest<>).MakeGenericType(requestType);
            object handler;
            try
            {
                handler = serviceProvider.GetService(serviceType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(HandlerNotFound + requestType, ex);
            }
            if (handler == null)
            {
                throw new InvalidOperationException(HandlerNotFound + requestType);
            }
            AddBus(handler, bus);
            return WrapRequestHandler(requestType, handler);
        }

        [DebuggerStepThrough, DebuggerHidden]
        internal static RequestHandlerWithResponse<TResponse> GetRequestHandler<TResponse>(this IServiceProvider serviceProvider, Type requestType, IBus bus = null)
        {
            var serviceType = typeof(IHandleRequest<,>).MakeGenericType(requestType, typeof(TResponse));
            object handler;
            try
            {
                handler = serviceProvider.GetService(serviceType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(HandlerNotFound + requestType, ex);
            }
            if (handler == null)
            {
                throw new InvalidOperationException(HandlerNotFound + requestType);
            }
            AddBus(handler, bus);
            return WrapRequestHandlerWithResponse<TResponse>(requestType, handler);
        }

        private static RequestHandler WrapRequestHandler(Type requestType, object handler)
        {
            var wrapperType = typeof(RequestHandler<>).MakeGenericType(requestType);
            return (RequestHandler)Activator.CreateInstance(wrapperType, handler);
        }

        internal abstract class RequestHandler
        {
            public abstract Task Handle(IRequest request, CancellationToken cancellationToken);
        }

        internal sealed class RequestHandler<TRequest> : RequestHandler
            where TRequest : IRequest
        {
            private readonly IHandleRequest<TRequest> _inner;

            public RequestHandler(IHandleRequest<TRequest> inner)
            {
                _inner = inner;
            }

            [DebuggerStepThrough, DebuggerHidden]
            public override async Task Handle(IRequest request, CancellationToken cancellationToken)
            {
                await _inner.Handle((TRequest)request, cancellationToken);
            }
        }

        private static RequestHandlerWithResponse<TResponse> WrapRequestHandlerWithResponse<TResponse>(Type requestType, object handler)
        {
            var wrapperType = typeof(RequestHandlerWithResponse<,>).MakeGenericType(requestType, typeof(TResponse));
            return (RequestHandlerWithResponse<TResponse>)Activator.CreateInstance(wrapperType, handler);
        }

        internal abstract class RequestHandlerWithResponse<TResponse>
        {
            public abstract Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken);
        }

        internal sealed class RequestHandlerWithResponse<TRequest, TResponse> : RequestHandlerWithResponse<TResponse>
            where TRequest : IRequest<TResponse>
        {
            private readonly IHandleRequest<TRequest, TResponse> _inner;

            public RequestHandlerWithResponse(IHandleRequest<TRequest, TResponse> inner)
            {
                _inner = inner;
            }

            [DebuggerStepThrough, DebuggerHidden]
            public override async Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken)
            {
                return await _inner.Handle((TRequest)request, cancellationToken);
            }
        }
        #endregion
    }
}
