using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Tests.Events;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture
{
    public class EventTests
    {
        private IBus CreateBus(Action<BusOptions> configure)
        {
            var services = new ServiceCollection();
            services.AddBus(configure);
            var provider = services.BuildServiceProvider();
            return provider.GetService<IBus>();
        }

        [Fact]
        public async Task SendSimpleEventTest()
        {
            var bus = CreateBus(o =>
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
            var bus = CreateBus(o =>
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
            var bus = CreateBus(o => { });
            await bus.Notify(new SimpleEvent());
        }
    }
}
