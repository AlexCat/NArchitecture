using Microsoft.Extensions.DependencyInjection;
using System;

namespace NArchitecture.Tests
{
    public static class BusFactory
    {
        public static IBus CreateBus(Action<BusOptions> configure)
        {
            var services = new ServiceCollection();
            services.AddBus(configure);
            var provider = services.BuildServiceProvider();
            return provider.GetService<IBus>();
        }

        public static IBus CreateTestBus()
        {
            return new TestBus();
        }
    }
}
