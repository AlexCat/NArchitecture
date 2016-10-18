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
            var requestService = BusFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<SimpleRequestHandler>();
            });

            await requestService.Request(BusFactory.CreateTestBus(), new SimpleRequest());
        }

        [Fact]
        public async Task SendSimpleRequestFailTest()
        {
            var requestService = BusFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<SimpleRequestHandlerFailing>();
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateTestBus(), new SimpleRequest());
            });
        }

        [Fact]
        public async Task SendSimpleRequestWithoutHandlerTest()
        {
            var requestService = BusFactory.CreateRequestService(o => { });
            
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateTestBus(), new SimpleRequest());
            });
        }

        [Fact]
        public async Task SendSimpleRequestWithTooManyHandlersTest()
        {
            var requestService = BusFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<SimpleRequestHandler>();
                o.AddRequestHandler<SimpleRequestHandlerFailing>();
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateTestBus(), new SimpleRequest());
            });
        }

        [Fact]
        public async Task SendComplexRequest()
        {
            var requestService = BusFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<ComplexRequestHandler>();
            });

            await requestService.Request(BusFactory.CreateTestBus(), new ComplexRequest());
        }

        [Fact]
        public async Task SendComplexRequestFailTest()
        {
            var requestService = BusFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<ComplexRequestHandlerFailing>();
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateTestBus(), new ComplexRequest());
            });
        }

        [Fact]
        public async Task SendComplexRequestWithoutHandlerTest()
        {
            var requestService = BusFactory.CreateRequestService(o => { });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateTestBus(), new ComplexRequest());
            });
        }

        [Fact]
        public async Task SendComplexRequestWithTooManyHandlersTest()
        {
            var requestService = BusFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<ComplexRequestHandler>();
                o.AddRequestHandler<ComplexRequestHandlerFailing>();
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateTestBus(), new ComplexRequest());
            });
        }
    }
}
