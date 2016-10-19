using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class DefaultRequestServiceTests
    {
        [Fact(DisplayName = "RequestService can handle request without response")]
        public async Task SendSimpleRequest()
        {
            var handler = A.Fake<IRequestHandler>();
            var bus = A.Fake<IBus>();
            var request = A.Fake<IRequest>();
            var service = new DefaultRequestService(new IRequestHandler[] { handler });

            A.CallTo(() => handler.CanHandle(request)).Returns(true);

            await service.Request(bus, request);

            A.CallTo(() => handler.Handle(A<RequestHandlerContext>.Ignored, request)).MustHaveHappened();
        }

        [Fact(DisplayName = "RequestService throws exception from handler for request without response")]
        public async Task SendSimpleRequestFailTest()
        {
            var handler = A.Fake<IRequestHandler>();
            var bus = A.Fake<IBus>();
            var request = A.Fake<IRequest>();
            var service = new DefaultRequestService(new IRequestHandler[] { handler });

            A.CallTo(() => handler.CanHandle(request)).Returns(true);
            A.CallTo(() => handler.Handle(A<RequestHandlerContext>.Ignored, request)).Throws<InvalidOperationException>();

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return service.Request(bus, request);
            });
        }

        [Fact(DisplayName = "RequestService throws exception if there is no handler for given request without response")]
        public async Task SendSimpleRequestWithoutHandlerTest()
        {
            var bus = A.Fake<IBus>();
            var request = A.Fake<IRequest>();
            var service = new DefaultRequestService(new IRequestHandler[0]);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return service.Request(bus, request);
            });
        }

        [Fact(DisplayName = "RequestService throws exception if there are too many handlers for given request without response")]
        public async Task SendSimpleRequestWithTooManyHandlersTest()
        {
            var handler1 = A.Fake<IRequestHandler>();
            var handler2 = A.Fake<IRequestHandler>();
            var bus = A.Fake<IBus>();
            var request = A.Fake<IRequest>();
            var service = new DefaultRequestService(new IRequestHandler[] { handler1, handler2 });

            A.CallTo(() => handler1.CanHandle(request)).Returns(true);
            A.CallTo(() => handler2.CanHandle(request)).Returns(true);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return service.Request(bus, request);
            });
        }

        [Fact(DisplayName = "RequestService can handle request")]
        public async Task SendComplexRequest()
        {
            var handler = A.Fake<IRequestHandler>();
            var bus = A.Fake<IBus>();
            var request = A.Fake<IRequest<int>>();
            var service = new DefaultRequestService(new IRequestHandler[] { handler });

            A.CallTo(() => handler.CanHandle(request)).Returns(true);

            await service.Request(bus, request);

            A.CallTo(() => handler.Handle(A<RequestHandlerContext>.Ignored, request)).MustHaveHappened();
        }

        [Fact(DisplayName = "RequestService throws exception from handler for request")]
        public async Task SendComplexRequestFailTest()
        {
            var handler = A.Fake<IRequestHandler>();
            var bus = A.Fake<IBus>();
            var request = A.Fake<IRequest<int>>();
            var service = new DefaultRequestService(new IRequestHandler[] { handler });

            A.CallTo(() => handler.CanHandle(request)).Returns(true);
            A.CallTo(() => handler.Handle(A<RequestHandlerContext>.Ignored, request)).Throws<InvalidOperationException>();

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return service.Request(bus, request);
            });
        }

        [Fact(DisplayName = "RequestService throws exception if there is no handler for given request")]
        public async Task SendComplexRequestWithoutHandlerTest()
        {
            var bus = A.Fake<IBus>();
            var request = A.Fake<IRequest<int>>();
            var service = new DefaultRequestService(new IRequestHandler[0]);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return service.Request(bus, request);
            });
        }

        [Fact(DisplayName = "RequestService throws exception if there are too many handlers for given request")]
        public async Task SendComplexRequestWithTooManyHandlersTest()
        {
            var handler1 = A.Fake<IRequestHandler>();
            var handler2 = A.Fake<IRequestHandler>();
            var bus = A.Fake<IBus>();
            var request = A.Fake<IRequest<int>>();
            var service = new DefaultRequestService(new IRequestHandler[] { handler1, handler2 });

            A.CallTo(() => handler1.CanHandle(request)).Returns(true);
            A.CallTo(() => handler2.CanHandle(request)).Returns(true);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
            {
                return service.Request(bus, request);
            });
        }
    }
}
