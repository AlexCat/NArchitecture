using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public class Bus : IBus
    {
        private readonly BusOptions options;
        private readonly IEventService eventService;
        private readonly IRequestService requestService;
        private readonly IValidationService validationService;
        private readonly IAuthorizationService authorizationService;

        public Bus(
            BusOptions options,
            IEventService eventService, 
            IRequestService requestService,
            IValidationService validationService,
            IAuthorizationService authorizationService)
        {
            Guard.AgainstNull(nameof(options), options);
            Guard.AgainstNull(nameof(eventService), eventService);
            Guard.AgainstNull(nameof(requestService), requestService);
            Guard.AgainstNull(nameof(validationService), validationService);
            Guard.AgainstNull(nameof(authorizationService), authorizationService);

            this.options = options;
            this.eventService = eventService;
            this.requestService = requestService;
            this.validationService = validationService;
            this.authorizationService = authorizationService;
        }

        public async Task<bool> Authorize(ClaimsPrincipal user, IMessage message)
        {
            var policyNames = options.GetMessagePolicies(message.GetType());
            var tasks = policyNames.Select(policyName => authorizationService.Authorize(user, message, policyName));
            var results = await Task.WhenAll(tasks);
            return results.All(r => r);
        }

        public Task Notify(IEvent @event)
        {
            return eventService.Notify(this, @event);
        }

        public Task Request(IRequest request)
        {
            return requestService.Request(this, request);
        }

        public Task<TResponse> Request<TResponse>(IRequest<TResponse> request)
        {
            return requestService.Request(this, request);
        }

        public Task Validate(IMessage message)
        {
            return validationService.Validate(message);
        }
    }
}
