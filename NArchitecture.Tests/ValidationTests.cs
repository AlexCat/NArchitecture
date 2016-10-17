using NArchitecture.Tests;
using NArchitecture.Tests.Validation;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture
{
    public class ValidationTests
    {
        [Fact]
        public async Task SimpleValidMessageTest()
        {
            var bus = BusFactory.CreateBus(o => { });

            await bus.Validate(new SimpleMessage { RequiredAttribute = "Simple Message" });
        }

        [Fact]
        public async Task SimpleInvalidMessageTest()
        {
            var bus = BusFactory.CreateBus(o => { });

            await Assert.ThrowsAsync<ValidationException>(() =>
            {
                return bus.Validate(new SimpleMessage());
            });
        }
    }
}
