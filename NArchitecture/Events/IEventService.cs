using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IEventService
    {
        Task Handle(IBus bus, IEvent @event);
    }
}
