using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class RequestHandlerTests
    {
        private class RequestWithoutResponse : IRequest { }

        private class RequestWithoutResponseHandler : RequestHandler<RequestWithoutResponse>
        {
            protected override Task Handle(RequestHandlerContext context, RequestWithoutResponse request)
            {
                return Task.FromResult(0);
            }
        }

        [Fact(DisplayName = "RequestHandler can confirm requests that it handles")]
        public void CanHandleTest()
        {
            var request = new RequestWithoutResponse();
            var handler = new RequestWithoutResponseHandler();
            Assert.True(handler.CanHandle(request));
        }

        private class RequestWithResponse : IRequest { }

        private class RequestWithResponseHandler : RequestHandler<RequestWithResponse>
        {
            protected override Task Handle(RequestHandlerContext context, RequestWithResponse request)
            {
                return Task.FromResult(0);
            }
        }

        [Fact(DisplayName = "RequestHandler can confirm requests with response that it handles")]
        public void CanHandleWithResponseTest()
        {
            var request = new RequestWithResponse();
            var handler = new RequestWithResponseHandler();
            Assert.True(handler.CanHandle(request));
        }
    }
}
