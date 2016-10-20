using FakeItEasy;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class RequestHandlerTests
    {
        private class Request : IRequest { }

        private class RequestHandler : RequestHandler<Request>
        {
            protected override Task Handle(RequestHandlerContext context, Request request)
            {
                Assert.NotNull(context.ServiceBus);
                return TaskCache.CompletedTask;
            }
        }

        [Fact(DisplayName = "RequestHandler can confirm requests")]
        public void CanHandleTest()
        {
            var request = new Request();
            var handler = new RequestHandler();
            Assert.True(handler.CanHandle(request));
        }

        [Fact(DisplayName = "RequestHandler handles correct requests")]
        public async Task HandlesTest()
        {
            var request = new Request();
            var handler = new RequestHandler();
            var context = A.Fake<RequestHandlerContext>();
            await handler.Handle(context, request);
        }

        [Fact(DisplayName = "RequestHandler can decline bad requests")]
        public void CannotHandleTest()
        {
            var request = new RequestWithResponse();
            var handler = new RequestHandler();
            Assert.False(handler.CanHandle(request));
        }

        [Fact(DisplayName = "RequestHandler throws on bad request")]
        public async Task ThrowsOnBadRequestTest()
        {
            var request = new RequestWithResponse();
            var handler = new RequestHandler();
            var context = A.Fake<RequestHandlerContext>();
            await Assert.ThrowsAsync<ArgumentException>(() =>
            {
                return handler.Handle(context, request);
            });
        }

        private class RequestWithResponse : IRequest<int> { }

        private class RequestWithResponseHandler : RequestHandler<RequestWithResponse, int>
        {
            protected override Task Handle(RequestHandlerContext<int> context, RequestWithResponse request)
            {
                Assert.NotNull(context.ServiceBus);
                context.Response = default(int);
                return TaskCache.CompletedTask;
            }
        }

        [Fact(DisplayName = "RequestHandler<> can confirm correct requests")]
        public void CanHandleWithResponseTest()
        {
            var request = new RequestWithResponse();
            var handler = new RequestWithResponseHandler();
            Assert.True(handler.CanHandle(request));
        }

        [Fact(DisplayName = "RequestHandler<> handles correct requests")]
        public async Task HandlesWithResponseTest()
        {
            var request = new RequestWithResponse();
            var handler = new RequestWithResponseHandler();
            var context = A.Fake<RequestHandlerContext<int>>();
            await handler.Handle(context, request);
        }

        [Fact(DisplayName = "RequestHandler<> can decline bad requests")]
        public void CannotHandleWithResponseTest()
        {
            var request = new Request();
            var handler = new RequestWithResponseHandler();
            Assert.False(handler.CanHandle(request));
        }

        [Fact(DisplayName = "RequestHandler<> throws on bad request")]
        public async Task ThrowsOnBadRequestWithResponseTest()
        {
            var request = new Request();
            var handler = new RequestWithResponseHandler();
            var context = A.Fake<RequestHandlerContext>();
            await Assert.ThrowsAsync<ArgumentException>(() =>
            {
                return handler.Handle(context, request);
            });
        }
    }
}
