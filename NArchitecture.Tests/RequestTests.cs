using NArchitecture.Tests.Requests;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class RequestTests
    {
        [Fact(DisplayName = "RequestService can handle request without response")]
        public async Task SendSimpleRequest()
        {
            var requestService = ServiceFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<SimpleRequestHandler>();
            });

            await requestService.Request(BusFactory.CreateBusMock(), new SimpleRequest());
        }

        [Fact(DisplayName = "RequestService throws exception from handler for request without response")]
        public async Task SendSimpleRequestFailTest()
        {
            var requestService = ServiceFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<SimpleRequestHandlerFailing>();
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateBusMock(), new SimpleRequest());
            });
        }

        [Fact(DisplayName = "RequestService throws exception if there is no handler for given request without response")]
        public async Task SendSimpleRequestWithoutHandlerTest()
        {
            var requestService = ServiceFactory.CreateRequestService(o => { });
            
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateBusMock(), new SimpleRequest());
            });
        }

        [Fact(DisplayName = "RequestService throws exception if there are too many handlers for given request without response")]
        public async Task SendSimpleRequestWithTooManyHandlersTest()
        {
            var requestService = ServiceFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<SimpleRequestHandler>();
                o.AddRequestHandler<SimpleRequestHandlerFailing>();
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateBusMock(), new SimpleRequest());
            });
        }

        [Fact(DisplayName = "RequestService can handle request")]
        public async Task SendComplexRequest()
        {
            var requestService = ServiceFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<ComplexRequestHandler>();
            });

            await requestService.Request(BusFactory.CreateBusMock(), new ComplexRequest());
        }

        [Fact(DisplayName = "RequestService throws exception from handler for request")]
        public async Task SendComplexRequestFailTest()
        {
            var requestService = ServiceFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<ComplexRequestHandlerFailing>();
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateBusMock(), new ComplexRequest());
            });
        }

        [Fact(DisplayName = "RequestService throws exception if there is no handler for given request")]
        public async Task SendComplexRequestWithoutHandlerTest()
        {
            var requestService = ServiceFactory.CreateRequestService(o => { });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateBusMock(), new ComplexRequest());
            });
        }

        [Fact(DisplayName = "RequestService throws exception if there are too many handlers for given request")]
        public async Task SendComplexRequestWithTooManyHandlersTest()
        {
            var requestService = ServiceFactory.CreateRequestService(o =>
            {
                o.AddRequestHandler<ComplexRequestHandler>();
                o.AddRequestHandler<ComplexRequestHandlerFailing>();
            });

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return requestService.Request(BusFactory.CreateBusMock(), new ComplexRequest());
            });
        }
    }
}
