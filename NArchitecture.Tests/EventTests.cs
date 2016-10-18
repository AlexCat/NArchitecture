using NArchitecture.Tests;
using NArchitecture.Tests.Events;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture
{
    public class EventTests
    {
        [Fact]
        public async Task SendSimpleEventTest()
        {
            var eventService = BusFactory.CreateEventService(o =>
            {
                o.AddEventHandler<SimpleEventHandler>();
            });

            await eventService.Notify(BusFactory.CreateTestBus(), new SimpleEvent());
        }

        [Fact]
        public async Task SendSimpleEventFailTest()
        {
            var eventService = BusFactory.CreateEventService(o =>
            {
                o.AddEventHandler<SimpleEventHandlerFailing>();
            });

            await Assert.ThrowsAsync<AggregateException>(() =>
            {
                return eventService.Notify(BusFactory.CreateTestBus(), new SimpleEvent());
            });
        }

        [Fact]
        public async Task SendSimpleEventWithoutHandlerTest()
        {
            var eventService = BusFactory.CreateEventService(o => { });
            await eventService.Notify(BusFactory.CreateTestBus(), new SimpleEvent());
        }
    }
}
