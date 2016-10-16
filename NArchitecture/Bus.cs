using System;
using System.Threading.Tasks;

namespace NArchitecture
{
    public class Bus : IBus
    {
        private readonly IEventService eventService;
        private readonly IRequestService requestService;
        private readonly IValidationService validationService;
        private readonly IAuthorizationService authorizationService;

        public Bus(
            IEventService eventService, 
            IRequestService requestService,
            IValidationService validationService,
            IAuthorizationService authorizationService)
        {
            Guard.AgainstNull(nameof(eventService), eventService);
            Guard.AgainstNull(nameof(requestService), requestService);
            Guard.AgainstNull(nameof(validationService), validationService);
            Guard.AgainstNull(nameof(authorizationService), authorizationService);

            this.eventService = eventService;
            this.requestService = requestService;
            this.validationService = validationService;
            this.authorizationService = authorizationService;
        }

        public Task<bool> Authorize(IMessage message)
        {
            throw new NotImplementedException();
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

        public Task Validate(IMessage message)
        {
            return validationService.Validate(message);
        }
    }
}
