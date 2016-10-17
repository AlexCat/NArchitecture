using Microsoft.Extensions.DependencyInjection;
using NArchitecture.Tests.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture
{
    public class ValidationTests
    {
        private IBus CreateBus(Action<BusOptions> configure)
        {
            var services = new ServiceCollection();
            services.AddBus(configure);
            var provider = services.BuildServiceProvider();
            return provider.GetService<IBus>();
        }

        [Fact]
        public async Task SimpleValidMessageTest()
        {
            var bus = CreateBus(o => { });

            await bus.Validate(new SimpleMessage { RequiredAttribute = "Simple Message" });
        }

        [Fact]
        public async Task SimpleInvalidMessageTest()
        {
            var bus = CreateBus(o => { });

            await Assert.ThrowsAsync<ValidationException>(() =>
            {
                return bus.Validate(new SimpleMessage());
            });
        }
    }
}
