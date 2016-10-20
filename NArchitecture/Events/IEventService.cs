using System.Threading.Tasks;

namespace NArchitecture
{
    public interface IEventService
    {
        Task Notify(IServiceBus bus, IEvent @event);
    }
}
