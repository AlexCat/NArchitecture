using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace NArchitecture.Tests
{
    public class ValidationTests
    {
        [Fact(DisplayName = "ValidationService returns true if validation succeeds")]
        public async Task SimpleValidMessageTest()
        {
            var validationService = ServiceFactory.CreateValidationService();

            await validationService.Validate(new SimpleMessage { RequiredAttribute = "Simple Message" });
        }

        [Fact(DisplayName = "ValidationService returns false if validation fails")]
        public async Task SimpleInvalidMessageTest()
        {
            var validationService = ServiceFactory.CreateValidationService();

            await Assert.ThrowsAsync<ValidationException>(() =>
            {
                return validationService.Validate(new SimpleMessage());
            });
        }
    }
}
