using NArchitecture.Tests;
using NArchitecture.Tests.Requests;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture
{
    public class RequestTests
    {
        [Fact]
        public async Task SendSimpleRequest()
        {
            var bus = BusFactory.CreateBus(o =>
            {
                o.ConfigureRequests(op =>
                {
                    op.AddRequestHandler<SimpleRequestHandler>();
                });
            });

            await bus.Request(new SimpleRequest());
        }

        [Fact]
        public async Task SendSimpleRequestFailTest()
        {
            var bus = BusFactory.CreateBus(o =>
            {
                o.ConfigureRequests(op =>
                {
                    op.AddRequestHandler<SimpleRequestHandlerFailing>();
                });
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return bus.Request(new SimpleRequest());
            });
        }

        [Fact]
        public async Task SendSimpleRequestWithoutHandlerTest()
        {
            var bus = BusFactory.CreateBus(o => { });
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return bus.Request(new SimpleRequest());
            });
        }

        [Fact]
        public async Task SendSimpleRequestWithTooManyHandlersTest()
        {
            var bus = BusFactory.CreateBus(o =>
            {
                o.ConfigureRequests(op =>
                {
                    op.AddRequestHandler<SimpleRequestHandler>();
                    op.AddRequestHandler<SimpleRequestHandlerFailing>();
                });
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return bus.Request(new SimpleRequest());
            });
        }

        [Fact]
        public async Task SendComplexRequest()
        {
            var bus = BusFactory.CreateBus(o =>
            {
                o.ConfigureRequests(op =>
                {
                    op.AddRequestHandler<ComplexRequestHandler>();
                });
            });

            await bus.Request(new ComplexRequest());
        }

        [Fact]
        public async Task SendComplexRequestFailTest()
        {
            var bus = BusFactory.CreateBus(o =>
            {
                o.ConfigureRequests(op =>
                {
                    op.AddRequestHandler<ComplexRequestHandlerFailing>();
                });
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return bus.Request(new ComplexRequest());
            });
        }

        [Fact]
        public async Task SendComplexRequestWithoutHandlerTest()
        {
            var bus = BusFactory.CreateBus(o => { });
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return bus.Request(new ComplexRequest());
            });
        }

        [Fact]
        public async Task SendComplexRequestWithTooManyHandlersTest()
        {
            var bus = BusFactory.CreateBus(o =>
            {
                o.ConfigureRequests(op =>
                {
                    op.AddRequestHandler<ComplexRequestHandler>();
                    op.AddRequestHandler<ComplexRequestHandlerFailing>();
                });
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return bus.Request(new ComplexRequest());
            });
        }
    }
}
