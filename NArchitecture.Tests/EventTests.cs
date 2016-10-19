using System;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class EventTests
    {
        [Fact(DisplayName = "EventService can notify handlers with event")]
        public async Task SendSimpleEventTest()
        {
            var eventService = ServiceFactory.CreateEventService(o =>
            {
                o.AddEventHandler<SimpleEventHandler>();
            });

            await eventService.Notify(BusFactory.CreateBusMock(), new SimpleEvent());
        }

        [Fact(DisplayName = "EventService correctly handles handler failure")]
        public async Task SendSimpleEventFailTest()
        {
            var eventService = ServiceFactory.CreateEventService(o =>
            {
                o.AddEventHandler<SimpleEventHandlerFailing>();
            });

            await Assert.ThrowsAsync<AggregateException>(() =>
            {
                return eventService.Notify(BusFactory.CreateBusMock(), new SimpleEvent());
            });
        }

        [Fact(DisplayName = "EventService correctly handles when there are no handlers")]
        public async Task SendSimpleEventWithoutHandlerTest()
        {
            var eventService = ServiceFactory.CreateEventService(o => { });
            await eventService.Notify(BusFactory.CreateBusMock(), new SimpleEvent());
        }
    }
}
