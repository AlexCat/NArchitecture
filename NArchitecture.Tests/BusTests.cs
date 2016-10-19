using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;


namespace NArchitecture.Tests
{
    public class BusTests
    {
        [Fact(DisplayName = "Bus routes the event to EventService")]
        public async Task BusNotifyTest()
        {
            var options = new BusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new Bus(options, events, requests, validation, authorization);

            var @event = A.Fake<IEvent>();
            await bus.Notify(@event);

            A.CallTo(() => events.Notify(bus, @event)).MustHaveHappened();
        }

        [Fact(DisplayName = "Bus routes the request to RequestService")]
        public async Task BusRequestTest()
        {
            var options = new BusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new Bus(options, events, requests, validation, authorization);

            var request = A.Fake<IRequest>();
            await bus.Request(request);

            A.CallTo(() => requests.Request(bus, request)).MustHaveHappened();
        }

        [Fact(DisplayName = "Bus routes the request with response to RequestService")]
        public async Task BusRequestWithResponseTest()
        {
            var options = new BusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new Bus(options, events, requests, validation, authorization);

            var request = A.Fake<IRequest<int>>();
            await bus.Request(request);

            A.CallTo(() => requests.Request(bus, request)).MustHaveHappened();
        }

        [Fact(DisplayName = "Bus routes the validation to ValidationService")]
        public async Task BusValidationTest()
        {
            var options = new BusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new Bus(options, events, requests, validation, authorization);

            var message = A.Fake<IMessage>();
            await bus.Validate(message);

            A.CallTo(() => validation.Validate(message)).MustHaveHappened();
        }

        [Fact(DisplayName = "Bus routes the authorization to AuthorizationService")]
        public async Task BusAuthorizationTest()
        {
            var options = new BusOptions();
            var events = A.Fake<IEventService>();
            var requests = A.Fake<IRequestService>();
            var validation = A.Fake<IValidationService>();
            var authorization = A.Fake<IAuthorizationService>();
            var bus = new Bus(options, events, requests, validation, authorization);

            var user = A.Fake<ClaimsPrincipal>();
            var message = A.Fake<IMessage>();
            options.AddPolicyFor(message.GetType(), "CustomPolicy");
            await bus.Authorize(user, message);

            A.CallTo(() => authorization.Authorize(user, message, "CustomPolicy")).MustHaveHappened();
        }
    }
}
