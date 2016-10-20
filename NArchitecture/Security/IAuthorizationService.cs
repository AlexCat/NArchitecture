using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IAuthorizationService
    {
        Task<bool> Authorize(IServiceBus bus, ClaimsPrincipal user, IMessage message, IEnumerable<IAuthorizationRequirement> requirements);
        Task<bool> Authorize(IServiceBus bus, ClaimsPrincipal user, IMessage message, string policyName);
    }
}
