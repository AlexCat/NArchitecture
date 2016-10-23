using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public class DefaultValidationService : IValidationService
    {
        public Task Validate(IServiceBus bus, ClaimsPrincipal user, IMessage message)
        {
            var items = new Dictionary<object, object>();
            items.Add("ServiceBus", bus);
            items.Add("User", user);
            var validationContext = new ValidationContext(message);
            Validator.ValidateObject(message, validationContext);
            return TaskCache.CompletedTask;
        }
    }
}
