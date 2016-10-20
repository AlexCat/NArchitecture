using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System;
using System.Threading.Tasks;

namespace NArchitecture.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact(DisplayName = "ServiceCollectionExtensions can add Bus")]
        public void AddServiceBusTest()
        {
            var services = new ServiceCollection();
            services.AddServiceBus(options => { });
            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<IServiceBus>());
        }

        public class EventHandler : IEventHandler
        {
            public Task Handle(EventHandlerContext context, IEvent @event)
            {
                throw new NotImplementedException();
            }
        }

        [Fact(DisplayName = "ServiceCollectionExtensions can add EventService")]
        public void AddEventServiceTest()
        {
            var services = new ServiceCollection();
            services.AddEventService(options => { options.AddEventHandler<EventHandler>(); });
            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<IEventService>());
        }

        public class RequestHandler : IRequestHandler
        {
            public bool CanHandle(IRequest request)
            {
                throw new NotImplementedException();
            }

            public Task Handle(RequestHandlerContext context, IRequest request)
            {
                throw new NotImplementedException();
            }
        }


        [Fact(DisplayName = "ServiceCollectionExtensions can add RequestService")]
        public void AddRequestServiceTest()
        {
            var services = new ServiceCollection();
            services.AddRequestService(options => { options.AddRequestHandler<RequestHandler>(); });
            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<IRequestService>());
        }

        public class AuthorizationHandler : IAuthorizationHandler
        {
            public Task Handle(AuthorizationHandlerContext context)
            {
                throw new NotImplementedException();
            }
        }

        [Fact(DisplayName = "ServiceCollectionExtensions can add AuthorizationService")]
        public void AddAuthorizationServiceTest()
        {
            var services = new ServiceCollection();
            services.AddAuthorizationService(options => { options.AddAuthorizationHandler<AuthorizationHandler>(); });
            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<IAuthorizationService>());
        }

        [Fact(DisplayName = "ServiceCollectionExtensions can add ValidationService")]
        public void AddValidationServiceTest()
        {
            var services = new ServiceCollection();
            services.AddValidationService();
            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<IValidationService>());
        }
    }
}
