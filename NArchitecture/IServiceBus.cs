using System.Security.Claims;
using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IServiceBus
    {
        Task<bool> Authorize(ClaimsPrincipal user, IMessage message);
        Task Notify(ClaimsPrincipal user, IEvent @event);
        Task Request(ClaimsPrincipal user, IRequest request);
        Task<TResponse> Request<TResponse>(ClaimsPrincipal user, IRequest<TResponse> request);
        Task Validate(ClaimsPrincipal user, IMessage message);
    }
}
