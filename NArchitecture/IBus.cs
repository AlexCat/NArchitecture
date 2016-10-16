using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IBus
    {
        Task Notify(IEvent @event);
        Task Request(IRequest request);
        Task<TResponse> Request<TResponse>(IRequest<TResponse> request);
    }
}
