using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NArchitecture
{
    public class DefaultValidationService : IValidationService
    {
        public Task Validate(IMessage message)
        {
            var validationContext = new ValidationContext(message);
            Validator.ValidateObject(message, validationContext);
            return TaskCache.CompletedTask;
        }
    }
}
