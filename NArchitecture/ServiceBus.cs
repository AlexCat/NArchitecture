using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public class ServiceBus : IServiceBus
    {
        private readonly ServiceBusOptions options;
        private readonly IEventService eventService;
        private readonly IRequestService requestService;
        private readonly IValidationService validationService;
        private readonly IAuthorizationService authorizationService;

        public ServiceBus(
            ServiceBusOptions options,
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

        public Task<bool> Authorize(ClaimsPrincipal user, IMessage message)
        {
            var policyName = options.GetPolicyFor(message.GetType());
            if (policyName != null)
            {
                return authorizationService.Authorize(this, user, message, policyName);
            }
            else
            {
                return Task.FromResult(true);
            }
        }

        public Task Notify(ClaimsPrincipal user, IEvent @event)
        {
            return eventService.Notify(this, user, @event);
        }

        public Task Request(ClaimsPrincipal user, IRequest request)
        {
            return requestService.Request(this, user, request);
        }

        public Task<TResponse> Request<TResponse>(ClaimsPrincipal user, IRequest<TResponse> request)
        {
            return requestService.Request<TResponse>(this, user, request);
        }

        public Task Validate(ClaimsPrincipal user, IMessage message)
        {
            return validationService.Validate(this, user, message);
        }
    }
}
