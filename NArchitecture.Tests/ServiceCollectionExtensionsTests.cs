using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace NArchitecture.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact(DisplayName = "ServiceCollectionExtensions can add Bus")]
        public void AddBusTest()
        {
            var services = new ServiceCollection();
            services.AddBus(options => { });
            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<IBus>());
        }

        [Fact(DisplayName = "ServiceCollectionExtensions can add EventService")]
        public void AddEventServiceTest()
        {
            var services = new ServiceCollection();
            services.AddEventService(options => { });
            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<IEventService>());
        }

        [Fact(DisplayName = "ServiceCollectionExtensions can add RequestService")]
        public void AddRequestServiceTest()
        {
            var services = new ServiceCollection();
            services.AddRequestService(options => { });
            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<IRequestService>());
        }

        [Fact(DisplayName = "ServiceCollectionExtensions can add AuthorizationService")]
        public void AddAuthorizationServiceTest()
        {
            var services = new ServiceCollection();
            services.AddAuthorizationService(options => { });
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
