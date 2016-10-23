using FakeItEasy;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;


namespace NArchitecture.Tests
{
    public class ServiceBusTests
    {
        [Fact(DisplayName = "ServiceBus routes the event to EventService")]
        public async Task BusNotifyTest()
        {
            var options = new ServiceBusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new ServiceBus(options, events, requests, validation, authorization);
            var user = A.Fake<ClaimsPrincipal>();

            var @event = A.Fake<IEvent>();
            await bus.Notify(user, @event);

            A.CallTo(() => events.Notify(bus, user, @event)).MustHaveHappened();
        }

        [Fact(DisplayName = "ServiceBus routes the request to RequestService")]
        public async Task BusRequestTest()
        {
            var options = new ServiceBusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new ServiceBus(options, events, requests, validation, authorization);
            var user = A.Fake<ClaimsPrincipal>();

            var request = A.Fake<IRequest>();
            await bus.Request(user, request);

            A.CallTo(() => requests.Request(bus, user, request)).MustHaveHappened();
        }

        [Fact(DisplayName = "ServiceBus routes the request with response to RequestService")]
        public async Task BusRequestWithResponseTest()
        {
            var options = new ServiceBusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new ServiceBus(options, events, requests, validation, authorization);
            var user = A.Fake<ClaimsPrincipal>();

            var request = A.Fake<IRequest<int>>();
            await bus.Request(user, request);

            A.CallTo(() => requests.Request(bus, user, request)).MustHaveHappened();
        }

        [Fact(DisplayName = "ServiceBus routes the validation to ValidationService")]
        public async Task BusValidationTest()
        {
            var options = new ServiceBusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new ServiceBus(options, events, requests, validation, authorization);
            var user = A.Fake<ClaimsPrincipal>();

            var message = A.Fake<IMessage>();
            await bus.Validate(user, message);

            A.CallTo(() => validation.Validate(bus, user, message)).MustHaveHappened();
        }

        [Fact(DisplayName = "ServiceBus routes the authorization to AuthorizationService")]
        public async Task BusAuthorizationTest()
        {
            var options = new ServiceBusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new ServiceBus(options, events, requests, validation, authorization);

            var user = A.Fake<ClaimsPrincipal>();
            var message = A.Fake<IMessage>();
            options.AddPolicyFor(message.GetType(), "CustomPolicy");
            await bus.Authorize(user, message);

            A.CallTo(() => authorization.Authorize(bus, user, message, "CustomPolicy")).MustHaveHappened();
        }
    }
}
