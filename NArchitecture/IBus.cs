using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IBus
    {
        Task<bool> Authorize(IMessage message);
        Task Notify(IEvent @event);
        Task Request(IRequest request);
        Task<TResponse> Request<TResponse>(IRequest<TResponse> request);
        Task Validate(IMessage message);
    }
}
