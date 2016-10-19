using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IEventService
    {
        Task Notify(IBus bus, IEvent @event);
    }
}
