using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class EventHandlerTests
    {
        private class Event : IEvent { }

        private class EventHandler : EventHandler<Event>
        {
            protected override Task Handle(EventHandlerContext context, Event @event)
            {
                Assert.NotNull(context.ServiceBus);
                return TaskCache.CompletedTask;
            }
        }

        [Fact(DisplayName = "EventHandler handles only given type")]
        public async Task CanHandleTest()
        {
            var @event = new Event();
            var handler = new EventHandler();
            var context = A.Fake<EventHandlerContext>();
            await handler.Handle(context, @event);
        }
    }
}
