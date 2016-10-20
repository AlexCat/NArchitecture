using FakeItEasy;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class DefaultValidationServiceTests
    {
        private class ValidatableMessage : IMessage
        {
            [Required]
            public string Name { get; set; }
        }

        [Fact(DisplayName = "ValidationService returns true if validation succeeds")]
        public async Task ValidateTest()
        {
            var bus = A.Fake<IServiceBus>();

            var service = new DefaultValidationService();

            await service.Validate(bus, new ValidatableMessage { Name = "ValidatableMessage" });
        }

        [Fact(DisplayName = "ValidationService returns false if validation fails")]
        public async Task ValidateFailedTest()
        {
            var bus = A.Fake<IServiceBus>();

            var service = new DefaultValidationService();

            await Assert.ThrowsAsync<ValidationException>(() =>
            {
                return service.Validate(bus, new ValidatableMessage());
            });
        }
    }
}
