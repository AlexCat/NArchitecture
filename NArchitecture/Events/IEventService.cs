using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IEventService
    {
        Task Notify(IServiceBus bus, ClaimsPrincipal user, IEvent @event);
    }
}
