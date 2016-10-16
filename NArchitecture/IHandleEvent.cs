using System.Threading;
using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IHandleEvent<in TEvent> : IHandle
        where TEvent : IEvent
    {
        Task Handle(
            TEvent message, 
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
