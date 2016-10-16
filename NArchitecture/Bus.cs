using System.Threading.Tasks;

namespace NArchitecture
{
    public class Bus : IBus
    {
        private readonly IEventService eventService;
        private readonly IRequestService requestService;

        public Bus(IEventService eventService, IRequestService requestService)
        {
            Guard.AgainstNull(nameof(eventService), eventService);
            Guard.AgainstNull(nameof(requestService), requestService);

            this.eventService = eventService;
            this.requestService = requestService;
        }

        public Task Notify(IEvent @event)
        {
            return eventService.Handle(this, @event);
        }

        public Task Request(IRequest request)
        {
            return requestService.Send(this, request);
        }

        public Task<TResponse> Request<TResponse>(IRequest<TResponse> request)
        {
            return requestService.Send(this, request);
        }
    }
}
