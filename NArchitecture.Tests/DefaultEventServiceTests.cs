using System;
using System.Threading.Tasks;
using Xunit;
using FakeItEasy;
using System.Security.Claims;

namespace NArchitecture.Tests
{
    public class DefaultEventServiceTests
    {
        [Fact(DisplayName = "EventService can notify handlers with event")]
        public async Task NotifyTest()
        {
            var handler = A.Fake<IEventHandler>();
            var bus = A.Fake<IServiceBus>();
            var user = A.Fake<ClaimsPrincipal>();
            var @event = A.Fake<IEvent>();

            var service = new DefaultEventService(new IEventHandler[] { handler });

            await service.Notify(bus, user, @event);

            A.CallTo(() => handler.Handle(A<EventHandlerContext>.Ignored, @event)).MustHaveHappened();
        }

        [Fact(DisplayName = "EventService correctly handles handler failure")]
        public async Task NotifyFailedTest()
        {
            var handler = A.Fake<IEventHandler>();
            var bus = A.Fake<IServiceBus>();
            var user = A.Fake<ClaimsPrincipal>();
            var @event = A.Fake<IEvent>();

            var service = new DefaultEventService(new IEventHandler[] { handler });

            A.CallTo(() => handler.Handle(A<EventHandlerContext>.Ignored, @event)).Throws<InvalidOperationException>();

            await Assert.ThrowsAsync<AggregateException>(() =>
            {
                return service.Notify(bus, user, @event);
            });
        }

        [Fact(DisplayName = "EventService correctly handles when there are no handlers")]
        public async Task NotifyWithoutHandlerTest()
        {
            var bus = A.Fake<IServiceBus>();
            var user = A.Fake<ClaimsPrincipal>();
            var @event = A.Fake<IEvent>();

            var service = new DefaultEventService(new IEventHandler[0]);

            await service.Notify(bus, user, @event);
        }
    }
}
