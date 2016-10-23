using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IValidationService
    {
        Task Validate(IServiceBus bus, ClaimsPrincipal user, IMessage message);
    }
}
