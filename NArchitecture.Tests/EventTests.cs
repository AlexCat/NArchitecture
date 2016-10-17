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
            var bus = BusFactory.CreateBus(o =>
            {
                o.ConfigureEvents(eo =>
                {
                    eo.AddEventHandler<SimpleEventHandler>();
                });
            });

            await bus.Notify(new SimpleEvent());
        }

        [Fact]
        public async Task SendSimpleEventFailTest()
        {
            var bus = BusFactory.CreateBus(o =>
            {
                o.ConfigureEvents(eo =>
                {
                    eo.AddEventHandler<SimpleEventHandlerFailing>();
                });
            });

            await Assert.ThrowsAsync<AggregateException>(() =>
            {
                return bus.Notify(new SimpleEvent());
            });
        }

        [Fact]
        public async Task SendSimpleEventWithoutHandlerTest()
        {
            var bus = BusFactory.CreateBus(o => { });
            await bus.Notify(new SimpleEvent());
        }
    }
}
